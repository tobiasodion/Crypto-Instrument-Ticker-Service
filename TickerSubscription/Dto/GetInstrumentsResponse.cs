namespace TickerSubscription.Dto;

/// <summary>
/// Response for the get instruments request.
/// </summary>
public class GetInstrumentResponse
{
    /// <summary>
    /// The instrument from Deribit.
    /// </summary>
    public string instrument_name { get; set; }
}