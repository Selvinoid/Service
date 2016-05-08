namespace Moodroon.DataLayer.Core
{
    using DataAccessLayer.Repositories;
    using DataAccessLayer.Repositories.Interfaces;

    using Microsoft.Practices.Unity;

    public class UnityFactory
    {
        public IUnityContainer Container { get; set; }

        private static UnityFactory _unityFactory;

        public static UnityFactory Instance
        {
            get
            {
                if (_unityFactory == null)
                {
                    lock (typeof(IUnityContainer))
                    {
                        if (_unityFactory == null)
                            _unityFactory = new UnityFactory();
                    }
                }
                return _unityFactory;
            }
        }
        private UnityFactory()
        {
            this.Container = new UnityContainer();
            this.Container.RegisterType<IHotelDbContext, HotelDbContext>("HotelEntities");
            this.Container.RegisterType<IUnitOfWork, UnitOfWork>("UnitOfWork", new InjectionConstructor(new ResolvedParameter<IHotelDbContext>("HotelEntities")));
        }
    }
}
