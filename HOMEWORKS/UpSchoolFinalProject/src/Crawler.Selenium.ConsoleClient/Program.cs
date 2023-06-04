using Application.Common.Models.Excel;
using Application.Features.OrderEvents.Commands.Add;
using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Commands.Update;
using Application.Features.Products.Commands.Add;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text;
using Application.Common.Models.Dtos;


bool Continue = false;

using var httpClient = new HttpClient();

List<Product> productsList = new List<Product>();

var hubConnection = new HubConnectionBuilder()
    .WithUrl($"https://localhost:7243/Hubs/SeleniumLogHub")
    .WithAutomaticReconnect()
    .Build();

await hubConnection.StartAsync();

SeleniumLogDto CreateLog(string message) => new SeleniumLogDto(message);

while (!Continue)
{
    // User internet connection check

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Internet connection checked."));

    if (!NetworkInterface.GetIsNetworkAvailable())
    {
        Console.WriteLine("Internet connection not found. Unable to start product scraping.");
        continue;
    }

    // User preferences

    Console.WriteLine("How many items do you want to engrave? (You can give a number or write 'all'.)");
    var requestedAmount = Console.ReadLine();

    Console.WriteLine("What type of products do you want to engrave?");
    Console.WriteLine("A-) All Products");
    Console.WriteLine("B-) Discounted Products");
    Console.WriteLine("C-) Non Discounted Products");
    var productCrawlType = Console.ReadLine();


    Console.WriteLine("Do you want the scraped products as a result of the application to be transferred to you via e-mail as an excel file? Y/N");
    var sendtToEmail = Console.ReadLine();


    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("User preferences received."));


    var orderAddRequest = new OrderAddCommand();

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Order request made."));

    bool userPreferences = false;

    while (!userPreferences)
    {

        switch (productCrawlType.ToUpper())
        {
            case "A":
                orderAddRequest = new OrderAddCommand()
                {
                    Id = Guid.NewGuid(),
                    ProductCrawlType = ProductCrawlType.All,
                    CreatedOn = DateTimeOffset.Now,
                    TotalFoundAmount = 0,
                    RequestedAmount = 0,

                };
                userPreferences = true;
                break;
            case "B":
                orderAddRequest = new OrderAddCommand()
                {
                    Id = Guid.NewGuid(),
                    ProductCrawlType = ProductCrawlType.IsDiscount,
                    CreatedOn = DateTimeOffset.Now,
                    TotalFoundAmount = 0,
                    RequestedAmount = 0,
                };
                userPreferences = true;
                break;
            case "C":
                orderAddRequest = new OrderAddCommand()
                {
                    Id = Guid.NewGuid(),
                    ProductCrawlType = ProductCrawlType.NonDiscount,
                    CreatedOn = DateTimeOffset.Now,
                    TotalFoundAmount = 0,
                    RequestedAmount = 0,
                };
                userPreferences = true;
                break;
            default:
                Console.WriteLine("Invalid option!");
                Thread.Sleep(1500);
                Console.Clear();
                break;

                await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.CrawlingFailed.ToString()));
        }
    }

    var orderAddResponse = await SendHttpPostRequest<OrderAddCommand, object>(httpClient, "https://localhost:7243/api/Orders/Add", orderAddRequest);
    Guid orderId = orderAddRequest.Id;


    // Settings and routing

    ChromeOptions options = new ChromeOptions();
    options.AddArgument("--start-maximized");
    options.AddArgument("--disable-notifications");
    options.AddArgument("--disable-popup-blocking");

    var Driver = new ChromeDriver(options);

    var Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

    Console.Clear();

    Driver.Navigate().GoToUrl("https://finalproject.dotnet.gg/");

    var orderEventAddRequest = new OrderEventAddCommand()
    {
        OrderId = orderId,
        Status = OrderStatus.BotStarted,
    };

    var orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.BotStarted.ToString()));

    IWebElement pageCountElement = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".pagination > li:nth-last-child(2) > a")));
   
    int pageCount = int.Parse(pageCountElement.Text);

    Console.WriteLine($"{pageCount} number of pages available.");
    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{pageCount} number of pages available."));
    Console.WriteLine("---------------------------------------");

    int itemCount = 0;

    orderEventAddRequest = new OrderEventAddCommand()
    {
        OrderId = orderId,
        Status = OrderStatus.CrawlingStarted,
    };

    orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.CrawlingStarted.ToString()));

    for (int i = 1; i <= pageCount; i++)
    {
        Driver.Navigate().GoToUrl($"https://finalproject.dotnet.gg/?currentPage={i}");

        Console.WriteLine($"{i}. Page");

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{i}.page was scanned."));

        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        Thread.Sleep(500);

        IReadOnlyCollection<IWebElement> productElements = Driver.FindElements(By.CssSelector(".card.h-100"));

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{productElements.Count} products found."));

        foreach (IWebElement productElement in productElements)
        {
            

            bool includeProduct = false;

            if (productCrawlType.ToUpper() == "A") // All option
            {
                includeProduct = true;
            }
            else if (productCrawlType.ToUpper() == "B") // Discounted products option
            {
                if (productElement.FindElements(By.CssSelector(".sale-price")).Any())
                    includeProduct = true;
            }
            else if (productCrawlType.ToUpper() == "C") // Regular priced products option
            {
                if (!productElement.FindElements(By.CssSelector(".sale-price")).Any())
                    includeProduct = true;
            }

            if (includeProduct)
            {
                // Get as many items as the user wants

                if (requestedAmount.ToLower() == "all" || itemCount < int.Parse(requestedAmount))
                {
                    string productName = productElement.FindElement(By.CssSelector(".fw-bolder.product-name")).GetAttribute("innerText");

                    string productPrice = productElement.FindElement(By.CssSelector(".price")).GetAttribute("innerText");

                    productPrice = productPrice.Replace("$", "").Replace(",", ".").Trim();

                    decimal price = decimal.Parse(productPrice, CultureInfo.InvariantCulture);

                    string productSalePrice = string.Empty;

                    IWebElement salePriceElement = null;

                    try
                    {
                        salePriceElement = productElement.FindElement(By.CssSelector(".sale-price"));
                    }
                    catch (NoSuchElementException)
                    {
                        // .sale-price element not found, product has no sale price
                    }

                    decimal salePrice = 0;

                    if (salePriceElement != null)
                    {
                        productSalePrice = salePriceElement.GetAttribute("innerText");

                        productSalePrice = productSalePrice.Replace("$", "").Replace(",", ".").Trim();

                        salePrice = decimal.Parse(productSalePrice, CultureInfo.InvariantCulture);
                    }

                    bool isOnSale = productElement.FindElements(By.CssSelector(".sale-price")).Count > 0;

                    string pictureUrl = productElement.FindElement(By.CssSelector(".card-img-top")).GetAttribute("src");

                    Console.WriteLine("Product Name: " + productName);
                    Console.WriteLine("Is On Sale?: " + isOnSale);

                    if (isOnSale)
                    {
                        Console.WriteLine("Sale Price: " + salePrice);
                    }
                    else
                    {
                        Console.WriteLine("Price: No discount!");
                    }

                    Console.WriteLine("Price: " + price);
                    Console.WriteLine("Picture: " + pictureUrl);
                    Console.WriteLine("----------------------------");

                    var productAddRequest = new ProductAddCommand()
                    {

                        OrderId = orderAddRequest.Id,
                        Name = productName,
                        Picture = pictureUrl,
                        IsOnSale = isOnSale,
                        Price = price,
                        SalePrice = salePrice,
                        CreatedOn = DateTimeOffset.Now

                     };

                    var product = new Product()
                    {

                        OrderId = orderAddRequest.Id,
                        Name = productName,
                        Picture = pictureUrl,
                        IsOnSale = isOnSale,
                        Price = price,
                        SalePrice = salePrice,
                        CreatedOn = DateTimeOffset.Now

                    };

                    var productAddResponse = await SendHttpPostRequest<ProductAddCommand, object>(httpClient, "https://localhost:7243/api/Products/Add", productAddRequest);

                    productsList.Add(product);

                    // ProductLog

                    await hubConnection.InvokeAsync("SendProductLogNotificationAsync", CreateLog($"Product Name : {productName}" + "   |    " +
                    $"Is On Sale ? :   {isOnSale}" + "   |    " +
                        $"Product Price :   {price}" + "   |    " +
                        $"Product Sale Price :   {salePrice}"));

                    itemCount++;
                }

            }

            var orderUpdateRequest = new OrderUpdateCommand()
            {
                Id = orderId,
                TotalFoundAmount = itemCount,
                RequestedAmount = requestedAmount,

            };

            var orderUpdateResponse = await SendHttpPostRequest<OrderUpdateCommand, object>(httpClient, "https://localhost:7243/api/Orders/Update", orderUpdateRequest);

        }
        

        if (sendtToEmail.ToUpper() == "Y")
        {
            Console.WriteLine("Please enter a valid e-mail address!");

            var userEmail = Console.ReadLine();

            ExcelModel excelModel = new ExcelModel();

            string recipientEmail = userEmail;

            string subject = "Excel file";

            string body = "Hello, there are products in the attached Excel file.";

            excelModel.ExportToExcel(productsList, recipientEmail, subject, body);
        }
        else if (sendtToEmail.ToUpper() == "N")
        {
            continue;
        }
        else
        {
            Console.WriteLine("Invalid login!");
            continue;
        }
    }


    Console.WriteLine($"{itemCount} products found.");
    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{itemCount} products of the requested type were found"));


    orderEventAddRequest = new OrderEventAddCommand()
    {
        OrderId = orderId,
        Status = OrderStatus.CrawlingCompleted,
    };

    orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.CrawlingCompleted.ToString()));

    orderEventAddRequest = new OrderEventAddCommand()
    {
        OrderId = orderId,
        Status = OrderStatus.OrderCompleted,
    };

    orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.OrderCompleted.ToString()));

    Console.WriteLine("Do you want to continue product scraping? (Y/N)");

    var choiceContinue = Console.ReadLine();

    if (choiceContinue.ToUpper()=="Y")
    {
        Driver.Dispose();

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot Will Restart!"));
    }
    else if (choiceContinue.ToUpper() == "N")
    {
        Driver.Dispose();

        httpClient.Dispose();

        Continue = true;

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot Stopped!"));
    }
}

await hubConnection.StopAsync();

async Task<TResponse> SendHttpPostRequest<TRequest, TResponse>(HttpClient httpClient, string url, TRequest payload)
{
    var jsonPayload = JsonConvert.SerializeObject(payload);

    var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    var response = await httpClient.PostAsync(url, httpContent);

    response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadAsStringAsync();

    var responseObject = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
    
    return responseObject;
}
