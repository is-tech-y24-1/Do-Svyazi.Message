# Message module

[![Build](https://github.com/is-tech-y24-1/Do-Svyazi.Message/actions/workflows/dotnet.yml/badge.svg)](https://github.com/is-tech-y24-1/Do-Svyazi.Message/actions/workflows/dotnet.yml)
![DotNet](https://img.shields.io/badge/dotnet%20version-net6.0-blue)

## Description 
Модуль реализует логику работы с сообщениями. 
- CRUD операции над сообщениями.
- Получение списка сообщений с пагинацией.

Наш сервис доступен пользователям из модуля 
[User](https://github.com/is-tech-y24-1/Do-Svyazi.User), мы 
используем его для аутентификации пользователей и авторизации операций над сообщениями.

Сообщения имеют контент разного типа, поддерживается пересылка сообщений.

## Stack
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQLite](https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET%20Core%206%20-blueviolet?style=for-the-badge&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core%206%20-informational?style=for-the-badge&logo=dotnet)
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)

## Quickstart

```bash
git clone https://github.com/is-tech-y24-1/Do-Svyazi.Message.git &&
cd Do-Svyazi.Message &&
dotnet restore &&
dotnet run --project Source/Presentation/Server/Do-Svyazi.Message.Server.WebAPI/
```

## Configuration
Конфигурация делается через файл appsettings.json. \
Параметр `ConnectionStrings.Database` задаёт строку для подключения
к базе данных (по умолчанию в `Program.cs` используется SQLite).