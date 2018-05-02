using System.Collections.Generic;
using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.AvialableCredit
{
    public class AvialableCreditCommand : CommonCommand<AvialableCreditResponce, AvialableCreditRequest>
    {
        public AvialableCreditCommand(ILocalDb localDb, ICommandDelegate<AvialableCreditResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(AvialableCreditRequest model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.GetData<AvialableCreditResponce>("Credit/GetAvialable", null);

            CommonExecute(responce);
        }
    }
}
