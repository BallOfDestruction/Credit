using System.Net.Http;
using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.CustomPay
{
    public class CustomPayCommand : CommonCommand<CustomPayResponce, CustomPayRequest>
    {
        public CustomPayCommand(ILocalDb localDb, ICommandDelegate<CustomPayResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(CustomPayRequest model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.PostData<CustomPayResponce>("Credit/CustomPay", JsonConvert.SerializeObject(model));

            CommonExecute(responce);
        }
    }
}
