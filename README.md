# Xbto Coding Assignment

## Brief

Using C# .NET, write a process that will subscribe and persist in real time to Deribit future instrument ticker. If you have the time, write a second process that will be able to retrieve these persisted data. This second process should be able to run side by side with the first one. Deribit API documentation : https://docs.deribit.com/#ticker-instrument_name-interval

## Solution

The solution includes the following:

- SubscriptionWorker
    - Creates a WebSocket connection to the Deribit RPC-Json service
    - Retrieves all future instruments for all currencies
    - Subscribes to the instrument ticker for the instruments retrieved
- NotificationHandler - Persists instrument ticker notifications to the database
- RetrievalWorker
    - Retrieves persisted instruments from the database based on the interval specifified in `RetrievalWorkerSettings` section of appsettings.
    ```
    {
    "RetrievalIntervalInSec": 30,
    "TimeSpanIntervalInSec": 40
    }
    ```
    - where:
        - `RetrievalIntervalInSec` - Interval between successive retrievals 
        - `TimeSpanIntervalInSec` - Timespan of notification data to be retrieved.
    - Logs the retrieved data to the console

## To Test 

Run the unit tests by executing `make test`

## To Run - With Docker-Compose

### Requirements

- [Docker Engine](https://docs.docker.com/engine/install/)

### Steps

- Copy the .env.template file to a .env file by executing `cp .env.template .env`
- **Not Required in development:** Update the value of the `MONGO_ROOT_PASSWORD` variable(please follow the MongoDB password requirements)
- Start the application by executing `docker-compose up --build`
- Monitor the console for the output of the application
- Stop the docker run by executing `ctrl + C`

## To Run - With Makefile

**NB**: Use this method if you already have a running MongoDB instance.

### Requirements

- Configurations of the running MongoDB instance

### Steps

- Update the `TickerStoreSettings` section in the appSettings.Development.json with the configurations of the running MongoDB instance:

```
{
    "ConnectionString": "mongodb://<user>:<password>@<server>:<port>",
    "DatabaseName": "<Database name>",
    "collectionName": "<collection name>"
}
```

**NB:** If the running MongoDB instance does require auth credentials, update the connection string value in this format: `mongodb://<server>:<port>`

- Start the API by executing the command `make`
