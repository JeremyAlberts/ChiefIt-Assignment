# 🐂 YakShop

YakShop is a .NET Core Web API that simulates a fictional yak-based e-commerce system. It tracks a herd of yaks, calculates their milk and skin production over time, and allows orders to be placed based on available resources.

---

## 📦 Features

- Track yak herd state over time
- Calculate milk and skin output by day
- Submit and process customer orders
- Reset herd and stock data
- RESTful API endpoints

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 or VS Code
- [Postman](https://www.postman.com/) or `curl` for testing endpoints (optional)

### Installation

```bash
git clone https://github.com/yourusername/YakShop.git
cd YakShop

dotnet restore
dotnet build
dotnet run --project YakShop.Api
