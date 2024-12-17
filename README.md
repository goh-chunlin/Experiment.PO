# Demo Project for Blog Post: Configure Portable Object: Localisation in ASP.NET Core

## Objective
This demo is about configuring Portable Object (PO) localisation in .NET Web API. It also covers:
- Troubleshooting deployment issues when running the Dockerised app;
- Lessons learned from debugging and resolving localisation problems in a containerized environment.
- Best practices and tips for avoiding similar issues when working with PO localization in .NET Web API.

## Technical Specification
Language: C# 12 ([Reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning))\
Framework: .NET 8

## How to Run?
1. There are two ways of running this demo project.
   - Run on localhost:
     ```
     dotnet build
     dotnet run
     ```
   - Run of Docker:
     ```
     docker build -t weather-forecast-api .
     docker run -d -p 5001:80 --name weathear-forecast-api-container weather-forecast-api
     ```

2. Visit the endpoint at `/weatherforecast?locale=zh-CN`.

## Blog Post

Read more about this project at [https://cuteprogramming.wordpress.com/2024/12/17/configure-portable-object-localisation-in-net-8-web-api/](https://cuteprogramming.wordpress.com/2024/12/17/configure-portable-object-localisation-in-net-8-web-api/).
