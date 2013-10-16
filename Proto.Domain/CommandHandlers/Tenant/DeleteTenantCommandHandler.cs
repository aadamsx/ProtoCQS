using System;
using System.Data.Entity;
using Proto.Data;
using Proto.Domain.Commands;
using Proto.Domain.Commands.Tenants;
using Proto.Model.Entities;

namespace Proto.Domain.CommandHandlers.Tenant
{
    public class DeleteTenantCommandHandler : ICommandHandler<DeleteTenantCommand>
    {
        private ClientManagementContext db;
        public DeleteTenantCommandHandler(ClientManagementContext context)
        {
            db = context;
        }

        void ICommandHandler<DeleteTenantCommand>.Handle(DeleteTenantCommand command)
        {

            var tenant = db.Tenants.Find(command.TenantId);
            if (tenant == null)
            {
                // convention, 0 = no tenant found
                command.TenantId = 0;
                return;
            }

            db.Tenants.Remove(tenant);
            db.SaveChanges();
        }
    }
}
