# PaymentNotifications.MobileBux.WebApi

This project contains a Web API for receiving payment notifications from Mobile Bux.

## Running the API

To run the API you will need to have .NET Core 3.1 installed. From the command line you can build the project using the following command:

```
$ dotnet build
```

You can then run the API locally using:

```
$ dotnet run
```

## Configuration Secrets

The application requires two secrets in order to be able to run correctly. Specifically, in `appsettings.json` you need to add the connection string and name for your EventHub. (In production you may also choose to inject these values in a different way than putting them directly into `appsettings.json`).

## Testing the API

The API exposes one endpoint `POST /payment_notifications`. The JSON payload for this request contains a single parameter: 'Amount'. With the application running locally you can therefore test the API with something like the following:

```
$ curl -v -X POST -H 'Content-Type:application/json' -d '{\"Amount\": 100}' http://localhost:5000/payment_notifications
```

Alternatively, in PowerShell this would look something like:

```
$payment = @{
    Amount=100
}
$json = $payment | ConvertTo-Json
 
Invoke-RestMethod 'http://localhost:5000/payment_notification' -Method Post -Body $json -ContentType 'application/json'


## Other Notes

Because this exercise is being shared with candidates with varying amounts of C#/.NET experience the code has been written in as simple a manner as possible, minimizing cleverness and indirection in favour of simple, procedural code. If you have any feedback on how to make this codebase clearer though please share!
