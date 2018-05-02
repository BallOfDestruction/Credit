using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.PayCredit
{
    public class PayCreditCommand : CommonCommand<PayCreditResponce, PayCreditRequest>
    {
        public PayCreditCommand(ILocalDb localDb, ICommandDelegate<PayCreditResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(PayCreditRequest model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.PostData<PayCreditResponce>("Credit/Pay", JsonConvert.SerializeObject(model));

            CommonExecute(responce);
        }
    }
}
