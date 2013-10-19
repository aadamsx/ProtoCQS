﻿using Data;
using Domain.Commands.Tenants;

namespace Domain.CommandHandlers.Tenant
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