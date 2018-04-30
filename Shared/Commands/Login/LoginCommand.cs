using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;
using Shared.Models;
using Shared.WebService;

namespace Shared.Commands.Login
{
    public class LoginCommand : CommonCommand<LoginResponceViewModel, LoginViewModel>
    {
        public LoginCommand(ILocalDb localDb, ICommandDelegate<LoginResponceViewModel> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(LoginViewModel model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.PostData<LoginResponceViewModel>("User/login/", JsonConvert.SerializeObject(model), false);

            if (responce.Answer != null)
            {
                LocalDatabase.AddNewItem(new LocalUserModel(responce.Answer));
            }

            CommonExecute(responce);
        }
    }
}
