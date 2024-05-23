add appsettings.json
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "your-db-connection-string"
  },
  "GmailSettings": {
    "FromEmail": "xxxx@gmail.com",
    "ToEmail": "xxxx@gmail.com", 
    "Password": "your-app-password"
  }
}
```
