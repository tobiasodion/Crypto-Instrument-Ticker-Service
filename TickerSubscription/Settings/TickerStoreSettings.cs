namespace TickerSubscription.Settings;

public class TickerStoreSettings
{
    /// <summary>
    /// Connection string for the database.
    /// </summary>
    public string ConnectionString { get; set; }
    /// <summary>
    /// Name of the database.
    /// </summary>
    public string DatabaseName { get; set; }
    /// <summary>
    /// Name of the collection.
    /// </summary>
    public string CollectionName { get; set; }
}