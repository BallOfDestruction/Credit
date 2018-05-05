using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.PayNow
{
    public class PayNowCommand : CommonCommand<PayNowResponce, PayNowRequest>
    {
        public PayNowCommand(ILocalDb localDb, ICommandDelegate<PayNowResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(PayNowRequest model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.PostData<PayNowResponce>("Credit/PayNow", JsonConvert.SerializeObject(model));

            CommonExecute(responce);
        }
    }
}
