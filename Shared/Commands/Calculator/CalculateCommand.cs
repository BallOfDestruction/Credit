using Newtonsoft.Json;
using Shared.Commands;
using Shared.Commands.CreateCredit;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Credit.Commands.Calculator
{
    public class CalculateCommand : CommonCommand<CalculateResponce, CalculateRequest>
    {
        public CalculateCommand(ILocalDb localDb, ICommandDelegate<CalculateResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(CalculateRequest model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.GetData<CalculateResponce>("Credit/Calculate", model);

            CommonExecute(responce);
        }
    }
}