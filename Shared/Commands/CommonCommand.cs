using Shared.Database;
using Shared.Delegates;
using Shared.WebService;

namespace Shared.Commands
{
    /// <typeparam name="T">Передаваемый тип в onsuccess</typeparam>
    /// <typeparam name="TE">Тип модели для выполнения</typeparam>
    public abstract class CommonCommand<T, TE> : ICommand<T, TE>
    {
        public ILocalDb LocalDatabase { get; set; }
        public ICommandDelegate<T> Delegate { get; set; }

        protected CommonCommand(ILocalDb localDb, ICommandDelegate<T> commandDelegate)
        {
            LocalDatabase = localDb;
            Delegate = commandDelegate;
        }

        public abstract void Execute(TE model);

        public void CommonExecute((T model, Error Error) model)
        {
            if (model.Error != null)
            {
                Delegate?.OnFailed?.Invoke(model.Error);
            }
            else
            {
                if (model.model == null)
                {
                    Delegate?.OnNotConnection?.Invoke();
                }
                else
                {
                    Delegate?.OnSuccess?.Invoke(model.model);
                }
            }
        }
    }
}