using System.Linq;
using DataAccessLayer.Models.Models;
using DataAccessLayer.Repositories.Repositories.DataBaseRepositories;
using Microsoft.Practices.Unity;
using Moodroon.DataLayer.Core.DTO;

namespace Moodroon.DataLayer.Core.Services
{
    public class ConditionService : BaseApplicationService
    {
        public ConditionService(IUnityContainer container) : base(container)
        {
        }

        public void DeleteCondition(int id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<ConditionRepository>();
                var condition = repo.GetSingle(p => p.Id == id);
                repo.Remove(condition);
                uow.SaveChanges();
            });
        }

        public void UploadImage(string url, int id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<ConditionRepository>();
                var condition = repo.GetSingle(p => p.Id == id);
                condition.Images.Add(new Image
                {
                    Url = url
                });
                repo.Modify(condition);
                uow.SaveChanges();
            });
        }

        public void RemoveImage(string url, int id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<ConditionRepository>();
                var condition = repo.GetSingle(p => p.Id == id);
                var image = condition.Images.FirstOrDefault(p => p.Url.Equals(url));
                condition.Images.Remove(image);
                repo.Modify(condition);
                uow.SaveChanges();
            });
        }

        public bool EditCondition(ConditionDto conditionDto)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<ConditionRepository>();
                var condition = repo.GetSingle(p => p.Id == conditionDto.Id);
                if (repo.Get().Any(p => p.Name.Equals(conditionDto.Name)))
                {
                    return false;
                }
                condition.Name = conditionDto.Name;
                repo.Modify(condition);
                uow.SaveChanges();
                return true;
            });
        }
    }
}
