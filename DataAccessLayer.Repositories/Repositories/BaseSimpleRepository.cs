namespace DataAccessLayer.Repositories.Repositories
{
    using System;

    using DataAccessLayer.Repositories.Interfaces;

    public class BaseSimpleRepository
    {
        public BaseSimpleRepository(IHotelDbContext dataContext)
        {
            this.Context = dataContext;
            if (this.Context == null)
            {
                throw new ArgumentException("Invalid context");
            }
        }

        protected IHotelDbContext Context
        {
            get;
            private set;
        }
    }
}
