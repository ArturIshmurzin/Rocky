using Braintree;

namespace Rocky_Utility.BrainTree
{
    public interface IBraneTreeGate
    {
        IBraintreeGateway CreateGateway();

        IBraintreeGateway GetGateway();
    }
}
