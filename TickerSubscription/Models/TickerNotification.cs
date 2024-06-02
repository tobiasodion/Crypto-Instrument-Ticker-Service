using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TickerSubscription.Models
{
    /// <summary>
    /// Representation of a single Instrument subscription notification.
    /// </summary>
    /// <param name="Name">Name of the Instrument.</param>
    /// <param name="Timestamp">Time stamp of data received.</param>
    /// <param name="Data">The snapshot data.</param>
    public record TickerNotification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        [BsonElement("name")]
        public string Name { get; init; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; init; }

        [BsonElement("data")]
        public string Data { get; init; }

        public TickerNotification(string name, DateTime timestamp, string data)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = name;
            Timestamp = timestamp;
            Data = data;
        }
    }
}