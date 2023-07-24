using Application.Common.Models.Dtos;
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
using System.Text;

namespace CrawlerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _seleniumLog = "https://localhost:7243/Hubs/SeleniumLogHub";
        private readonly string _orderHub = "https://localhost:7243/Hubs/OrderHub";
        private HubConnection seleniumLogHubConnection;
        private HubConnection orderHubConnection;



        public string RequestedAmountUser { get; set; }
        public string ProductCrawlTypeUser { get; set; }

        private bool _isCrawling;

        public Worker(ILogger<Worker> logger)
        {
            seleniumLogHubConnection = new HubConnectionBuilder()
                .WithUrl(_seleniumLog)
                .WithAutomaticReconnect()
                .Build();

            orderHubConnection = new HubConnectionBuilder()
                .WithUrl(_orderHub)
                .WithAutomaticReconnect()
                .Build();

            _logger = logger;
            _isCrawling = false;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                
                await Task.WhenAll(
                    seleniumLogHubConnection.StartAsync(stoppingToken),
                    orderHubConnection.StartAsync(stoppingToken)
                );

                if (seleniumLogHubConnection.State == HubConnectionState.Connected && orderHubConnection.State == HubConnectionState.Connected)
                {
                    seleniumLogHubConnection.On<string>("SendLogNotificationAsync", async (log) => { await Crawler(); });

                    orderHubConnection.On<string, string>("UserDataReceived", async (requestedAmount, productCrawlType) =>
                    {
                        RequestedAmountUser = requestedAmount;
                        ProductCrawlTypeUser = productCrawlType;

                        if (!_isCrawling) 
                        {
                            await Crawler();
                        }
                    });
                }
                else
                {
                    Console.WriteLine("SignalR baðlantýsý baþlatýlamadý.");
                }

                await Task.Delay(-1, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hata: " + e.Message);
            }
            finally
            {
                //await Task.WhenAll(
                //    seleniumLogHubConnection.StopAsync(),
                //    orderHubConnection.StopAsync()
                //);
            }
        }

        public async Task Crawler()
        {
            _isCrawling = true;

            // User internet connection check
            using var httpClient = new HttpClient();
            List<Product> productsList = new List<Product>();

            SeleniumLogDto CreateLog(string message) => new SeleniumLogDto(message);

            // User preferences
            var requestedAmount = RequestedAmountUser;

            var productCrawlType = ProductCrawlTypeUser;

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("User preferences received."));

            OrderAddCommand orderAddRequest = null;

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Order request made."));

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
                        
                        Thread.Sleep(1500);
                        Console.Clear();
                        break;
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

            Driver.Navigate().GoToUrl("https://4teker.net/");

            var orderEventAddRequest = new OrderEventAddCommand()
            {
                OrderId = orderId,
                Status = OrderStatus.BotStarted,
            };

            var orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.BotStarted.ToString()));

            IWebElement pageCountElement = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".pagination > li:nth-last-child(2) > a")));

            int pageCount = int.Parse(pageCountElement.Text);

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{pageCount} number of pages available."));

            int itemCount = 0;

            orderEventAddRequest = new OrderEventAddCommand()
            {
                OrderId = orderId,
                Status = OrderStatus.CrawlingStarted,
            };

            orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.CrawlingStarted.ToString()));

            for (int i = 1; i <= pageCount; i++)
            {
                Driver.Navigate().GoToUrl($"https://4teker.net/?currentPage={i}");

                await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{i}.page was scanned."));

                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

                Thread.Sleep(500);

                IReadOnlyCollection<IWebElement> productElements = Driver.FindElements(By.CssSelector(".card.h-100"));

                await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{productElements.Count} products found."));

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
                            await seleniumLogHubConnection.InvokeAsync("SendProductLogNotificationAsync", CreateLog($"{productName}"));

                            itemCount++;
                        }
                    }

                    var orderUpdateRequest = new OrderUpdateCommand()
                    {
                        Id = orderId,
                        TotalFoundAmount = itemCount,
                        RequestedAmount = requestedAmount.ToString(),
                    };

                    var orderUpdateResponse = await SendHttpPostRequest<OrderUpdateCommand, object>(httpClient, "https://localhost:7243/api/Orders/Update", orderUpdateRequest);
                }
            }

           
            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{itemCount} products of the requested type were found"));

            orderEventAddRequest = new OrderEventAddCommand()
            {
                OrderId = orderId,
                Status = OrderStatus.CrawlingCompleted,
            };

            orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.CrawlingCompleted.ToString()));

            orderEventAddRequest = new OrderEventAddCommand()
            {
                OrderId = orderId,
                Status = OrderStatus.OrderCompleted,
            };

            orderEventAddResponse = await SendHttpPostRequest<OrderEventAddCommand, object>(httpClient, "https://localhost:7243/api/OrderEvents/Add", orderEventAddRequest);

            await seleniumLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.OrderCompleted.ToString()));

            Driver.Dispose();

            httpClient.Dispose();

            _isCrawling = false;
        }

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
    }
}
