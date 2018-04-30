using Shared.Database;
using Shared.Delegates;

namespace Shared.Commands
{
    public interface ICommand<T, in TE>
    {
        ILocalDb LocalDatabase { get; set; }
        ICommandDelegate<T> Delegate { get; set; }
        void Execute(TE model);
    }
}
