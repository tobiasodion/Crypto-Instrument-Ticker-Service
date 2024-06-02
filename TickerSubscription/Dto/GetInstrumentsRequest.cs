using TickerSubscription.Enums;

namespace TickerSubscription.Dto;

/// <summary>
/// A get instrument request parameters.
/// </summary>
/// <param name="Currency">The currency symbol or "any" for all currencies</param>
/// <param name="Kind">Instrument kind, if not provided instruments of all kinds are considered</param>
/// <param name="Expired">Set to true to show recently expired instruments instead of active ones. Default is false</param>
public record GetInstrumentsRequest
(
    /// <summary>
    /// The currency type representation of the request.
    /// </summary>
    CurrencyType Currency,
    /// <summary>
    /// The instrument kind representation of the request.
    /// </summary>
    InstrumentKind Kind,
    /// <summary>
    /// The show recently expired instrument flag of the request.
    /// </summary>
    bool Expired = false
);