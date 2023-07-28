## UpCrawler - Capstone Project

Demo for UpCrawler

https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/32aa21b3-3b94-4252-a646-b233efa4ce8a


You can watch Capstone Presentation here https://drive.google.com/drive/folders/1S9eGbZdSWGt4P2MW9byJ5AcX_y3WSn9t

## Description

Crawler Bot is a C# .NET project designed to scrape product information from an e-commerce website and store it in a MySQL database. It utilizes a C# background worker to navigate the target website and extract details such as product names, prices, discount availability, and image URLs. The project follows the Clean Architecture and CQRS design patterns, ensuring a well-structured and maintainable codebase.

## Features

- Web scraping of product details: The Crawler Bot navigates to the e-commerce website and gathers product information, including regular and discounted prices, image URLs, and product names.
- Clean Architecture and CQRS: The project is structured according to the Clean Architecture principles, ensuring separation of concerns and maintainability. The CQRS pattern is implemented to segregate read and write operations effectively.
- User Management: Microsoft Identity is employed for user authentication, allowing users to register and log in through traditional methods or using Google login. JWT tokens are used for secure login/logout procedures.
- Email Notifications: The application sends email notifications to users upon registration and when specific product details are scraped.
- Global Exception Handling: A GlobalException filter is implemented to handle and manage exceptions gracefully throughout the application.
- Front-end with React and TypeScript: The front-end of the application is built using React with TypeScript, providing a responsive and user-friendly interface.
- Tailwind CSS for Styling: Tailwind CSS is used for designing the UI, ensuring a clean and modern appearance.

## Back-end Technologies

- C# .NET
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- Microsoft Identity for User Management
- JWT (JSON Web Tokens) for Authentication
- MySQL for Database Management
- Background Worker for Web Scraping
- SignalR for Real-Time Communication
- Selenium Framework for bots

## Front-end Technologies

- React with TypeScript
- Tailwind CSS for Styling

## How the Crawler Bot Works

1. **User Authentication**: Users can register and log in using traditional methods or through Google login. JWT tokens are issued for secure authentication.
2. **Homepage**: Upon successful login, users are directed to the home page, where they can directly navigate to give an order.
3. **Creating Orders**: Users can create new orders, specifying details such as the number of products to scrape and the type of products (all, discounted, non-discounted).
4. **SignalR Communication**: When an order is created, the details are sent to the back-end worker using SignalR hub for web scraping.
5. **Web Scraping**: The background worker, with the help of a web driver, navigates to the e-commerce website and performs web scraping based on the user's order details.
6. **Data Storage**: The scraped product information is stored in the "Product" table, and order-related information is stored in the "Order" table. Bot status and order completion details are saved in the "Order Event" table.
7. **Live Tracking**: Users can track the bot's progress and the scraped products in real-time using the "Live Track" page. SignalR facilitates the transfer of logs from the back-end to the front-end for live updates.
8. **Protected Routes**: The application uses protected routes to ensure user session security. If a token expires, the user is automatically logged out for enhanced security.

## Email Notification and Export to Excel

Users have the option to export their order details to an Excel table directly from the product page and send these crawled products via email. This feature enables users to conveniently analyze, store, and share their scraped data with ease.

## Installation and Setup

1. Clone the repository from GitHub.
2. Set up the necessary environment for C# .NET and React with TypeScript.
3. Install the required C# and JavaScript dependencies.
4. Configure the MySQL database connection settings.
5. Build and run the C# .NET back-end.
6. Start the React front-end to access the Crawler Bot application.

## Visuals from UpCrawler

- Login Page
  
  ![LoginPage](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/b3b05b88-497c-484e-afb0-763613737ba4)

- Register Page
  
  ![RegisterPage](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/31e0bb81-935b-46c8-a29f-72c1b2099cb5)

- Home Page
  
  ![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/8ab723a5-6abd-4eb0-b4fb-e4bb2e0343cd)
  
- Order Page
  
  ![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/1a1a6865-eb1b-4f6a-bf49-cf93f11c0655)

- Live Tracking
  
  ![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/a293a486-5712-4697-9622-c302a24c1290)

- Products Page
  
https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/62784ae2-c4e6-42ab-b874-6371699b3b1d

- Excel File
  
  ![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/fc787c65-17e5-4deb-9c77-d912268da0b2)

- Mail Content
  
  ![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/31a35c5e-250f-46f8-a8ee-daded1393d81)

- Web API
  
https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/91a13eb2-0c46-4daa-9090-0a00e98071be

- DB - ER Diagram
  
  ![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/23e42dfc-594d-4283-bddf-b286609a2492)
  
