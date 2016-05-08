namespace Moodroon.DataLayer.Core.Services
{
    using System;

    using DataAccessLayer.Repositories.Interfaces;

    using Microsoft.Practices.Unity;

    public abstract class BaseApplicationService : IDisposable
    {
        private readonly IUnityContainer container;
        

        protected BaseApplicationService(IUnityContainer container)
        {
            this.container = container;
        }

        public void Dispose()
        {
        }

        protected virtual TResult InvokeInUnitOfWorkScope<TResult>(Func<IUnitOfWork, TResult> func)
        {
            var result = default(TResult);

            this.TryInvokeServiceActionInUnitOfWorkScope(
                work =>
                {
                    result = func.Invoke(work);
                });

            return result;
        }

        protected virtual void InvokeInUnitOfWorkScope(Action<IUnitOfWork> action)
        {
            this.TryInvokeServiceActionInUnitOfWorkScope(action.Invoke);
        }

        private void TryInvokeServiceActionInUnitOfWorkScope(Action<IUnitOfWork> action)
        {
            this.TryInvokeServiceAction(
                () =>
                {
                    using (var unitOfWork = this.container.Resolve<IUnitOfWork>("UnitOfWork"))
                    {
                        action.Invoke(unitOfWork);
                    }
                });
        }

        private void TryInvokeServiceAction(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (InvalidOperationException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("cannot invoke service action", exception);
            }
        }
    }
}
