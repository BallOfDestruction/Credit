using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.Recalculating
{
    public class RecalculatingCommand : CommonCommand<RecalculatingResponce, RecalculatingRequest>
    {
        public RecalculatingCommand(ILocalDb localDb, ICommandDelegate<RecalculatingResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(RecalculatingRequest model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.PostData<RecalculatingResponce>("Credit/Recalculating", JsonConvert.SerializeObject(model));

            CommonExecute(responce);
        }
    }
}
