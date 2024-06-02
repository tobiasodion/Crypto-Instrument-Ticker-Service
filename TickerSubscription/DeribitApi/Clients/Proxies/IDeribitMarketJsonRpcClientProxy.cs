using System.Threading;
using System.Threading.Tasks;
using StreamJsonRpc;
using TickerSubscription.Dto;

namespace TickerSubscription.DeribitApi.Clients.Proxies;

/// <summary>
/// The dynamic client proxy for Deribit methods.
/// </summary>
/// <remarks>Contains all Market Data-related calls.</remarks>
public partial interface IDeribitJsonRpcClientProxy
{
    /// <summary>
    /// Method for retrieving data on all currencies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Collection of currency data.</returns>
    [JsonRpcMethod("public/get_currencies")]
    Task<GetCurrencyResponse[]> GetCurrencies(CancellationToken cancellationToken);

    /// <summary>
    /// Method for retrieving data on all instruments for a specific currency.
    /// </summary>
    /// <param name="currency">The currency.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Collection of instrument data.</returns>
    [JsonRpcMethod("public/get_instruments")]
    Task<GetInstrumentResponse[]> GetInstruments(string currency, string kind, bool expired, CancellationToken cancellationToken);
}