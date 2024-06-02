namespace TickerSubscription.Dto;

/// <summary>
/// Response for the get currencies request.
/// </summary>
public class GetCurrencyResponse
{
    /// <summary>
    /// The curry from Deribit.
    /// </summary>
    public string currency { get; set; }
}