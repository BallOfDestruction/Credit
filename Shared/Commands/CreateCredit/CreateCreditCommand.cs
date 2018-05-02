using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.CreateCredit
{
    public class CreateCreditCommand : CommonCommand<CreateCreditResponce, CreateCreditRequestModel>
    {
        public CreateCreditCommand(ILocalDb localDb, ICommandDelegate<CreateCreditResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(CreateCreditRequestModel model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.PostData<CreateCreditResponce>("Credit/Create", JsonConvert.SerializeObject(new CreateCreditRequest(model)));

            CommonExecute(responce);
        }
    }
}
