using System;
using TickerSubscription.Enums;

namespace TickerSubscription.Extensions;

public static class EnumExtensions
{
    public static string GetStringValue(this CurrencyType currencyType)
    {
        return currencyType switch
        {
            CurrencyType.BTC => "BTC",
            CurrencyType.ETH => "ETH",
            CurrencyType.USDC => "USDC",
            CurrencyType.USDT => "USDT",
            CurrencyType.EURR => "EURR",
            CurrencyType.ANY => "any",

            _ => throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null)
        };
    }

    public static string GetStringValue(this InstrumentKind instrumentKind)
    {
        return instrumentKind switch
        {
            InstrumentKind.FUTURE => "future",
            InstrumentKind.OPTION => "option",
            InstrumentKind.SPOT => "spot",
            InstrumentKind.FUTURE_COMBO => "future_combo",
            InstrumentKind.OPTION_COMBO => "option_combo",
            _ => throw new ArgumentOutOfRangeException(nameof(InstrumentKind), instrumentKind, null)
        };
    }
}
