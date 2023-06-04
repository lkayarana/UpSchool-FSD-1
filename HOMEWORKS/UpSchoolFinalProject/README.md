# UpSchool Crawler App



https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/6ce75eae-92ef-4dfa-b5de-3d61af0541a4



In UpSchool Crawler App, products on https://finalproject.dotnet.gg e-commerce site were scraping according to user's selections via Selenium Framework. User can select all of the products exist on the website or input a number to scrap and select crawl types as discounted products, nondiscounted products or all. After inputs were taken, UpSchool Crawler App requests to Web Api and starts to scraping for desired amount and type of products by generating a new order. Meanwhile, order events logs on wasm Blazor via SignalR.
By using Clean Architecture, real-time communication among application and database is set as initiating SignalR through application layer. Finally, crawled products can be converted to Excel file and send to user's e-mail address as request.



![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/8c9e22ce-85fc-4427-8f0e-a232840c0d9b

