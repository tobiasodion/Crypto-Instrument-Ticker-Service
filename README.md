# Xbto Coding Assignment

## Brief

Using C# .NET, write a process that will subscribe and persist in real time to Deribit future instrument ticker. If you have the time, write a second process that will be able to retrieve these persisted data. This second process should be able to run side by side with the first one. Deribit API documentation : https://docs.deribit.com/#ticker-instrument_name-interval

## Solution

The solution includes the following:

- SubscriptionWorker
    Creates a websocket connection to the Deribit RPC-Json service
    Retrieves all the currencies from the service
    Retrieves all instruments for currencies retrieved
    Subscribes to the instrument ticker for the instruments retrieved
- NotificationHandler - Persists instrument ticker notifications to the database
- RetrievalWorker
    Retrieves persisted instruments from the database
    Logs the data to the console

## To Run - With Docker-Compose

### Requirements

- [Docker Engine](https://docs.docker.com/engine/install/)

### Steps

- Copy the .env.template file to a .env file by executing `cp .env.template .env`
- **Not Required in development:** Update the value of the `MONGO_ROOT_PASSWORD` variable(please follow the mongodb password requirements)
- Start the application by executing `docker-compose up --build`
- Monitor the console for the output of the application
- Stop the docker run by executing `ctrl + C`

## To Run - With Makefile

**NB**: Use this method if you already running mongodb instance.

### Requirements

- Configurations of the running MongoDb instance

### Steps

- Update the `TickerStoreSettings` section in the appSettings.Development.json with the configuration of the running MongoDb instance:

```
{
    "ConnectionString": "mongodb://<user>:<password>@<server>:<port>",
    "DatabaseName": "<Database name>",
    "collectionName": "<collection name>"
}
```

**NB:** If the running MongoDb instance does not have auth credentials, update the connection string value in this format `mongodb://<server>:<port>`

- Start the API by executing the command `make`
