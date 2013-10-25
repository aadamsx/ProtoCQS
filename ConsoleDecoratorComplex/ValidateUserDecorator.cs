using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ProtoConsole
{
    public class ValidateUserDecorator<T> : IRepository<T>
    {
        private readonly IRepository<T> decoratee;
        //private readonly IUserSecurity userSecurity;
        private IPrincipal User { get; set; }

        public ValidateUserDecorator(
            IRepository<T> decoratee,
            IPrincipal principal)
        {
            this.decoratee = decoratee;
            User = principal;
        }

        public T ReadTById(object id)
        {
            if (!User.IsInRole("ValidRoleToExecute"))
                throw new ValidationException();
            return decoratee.ReadTById(id);
        }

        public IEnumerable<T> ReadTs()
        {
            if (!User.IsInRole("ValidRoleToExecute"))
                throw new ValidationException();

           return this.decoratee.ReadTs();
        }

        public void UpdateT(T entity)
        {
            if (!User.IsInRole("ValidRoleToExecute"))
                throw new ValidationException();
            this.decoratee.UpdateT(entity);
        }

        public void CreateT(T entity)
        {
            if (!User.IsInRole("ValidRoleToExecute"))
                throw new ValidationException();
            this.decoratee.CreateT(entity);
        }

        public void DeleteT(T entity)
        {
            if (!User.IsInRole("ValidRoleToExecute"))
                throw new ValidationException();
            this.decoratee.DeleteT(entity);
        }
    }
}