using Braintree;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_Utility.BrainTree
{
    public class BrainTreeGate : IBraneTreeGate
    {
        public BrainTreeGate(IOptions<BrainTreeSettings> options)
        {
            Options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        private IBraintreeGateway braintreeGateway { get; set; }

        public BrainTreeSettings Options { get; set; }

        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway(Options.Environment, Options.MerchantId, Options.PublicKey, Options.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (braintreeGateway == null)
                braintreeGateway = this.CreateGateway();

            return braintreeGateway;
        }
    }
}
