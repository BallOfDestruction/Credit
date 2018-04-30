using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.Registration
{
    public class RegistrationCommand : CommonCommand<RegistrationResponce, RegistrationRequest>
    {
        public RegistrationCommand(ILocalDb localDb, ICommandDelegate<RegistrationResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(RegistrationRequest model)
        {
            var client = new WSHttpClient();
            var responce = client.PostData<RegistrationResponce>("User/Registration/", JsonConvert.SerializeObject(model), false);
            CommonExecute(responce);
        }
    }
}
