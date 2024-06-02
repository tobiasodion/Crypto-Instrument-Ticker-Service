using System;

namespace TickerSubscription.DeribitApi.Clients.Exceptions;

public class UnattachedRpcClientException : Exception
{
    public override string Message => "A method call was attempted on an RPC client service that hasn't been attached.";
}