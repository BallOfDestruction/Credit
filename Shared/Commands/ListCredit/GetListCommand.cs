using System.Linq;
using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands.ListCredit
{
    public class GetListCommand : CommonCommand<GetListResponce, object>
    {
        public GetListCommand(ILocalDb localDb, ICommandDelegate<GetListResponce> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(object model)
        {
            var httpClient = new WSHttpClient();
            var responce = httpClient.GetData<GetListResponce>("Credit/GetList/", null);

            if (responce.Answer?.Credits?.Any() == true)
            {
                LocalDatabase?.DeleteAll<Models.Credit>();
                LocalDatabase?.AddNewItems(responce.Answer.Credits);
            }

            CommonExecute(responce);

            if (responce.Answer == null && responce.Error == null)
            {
                var credits = LocalDatabase.GetItems<Models.Credit>()?.ToList();
                if (credits?.Any() == true)
                {
                    Delegate?.OnSuccess?.Invoke(new GetListResponce(credits));
                }
            }
        }
    }
}
