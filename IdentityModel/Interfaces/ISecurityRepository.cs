﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetIdentity
{
    public interface ISecurityRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Delete(params object[] keyValues);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        int Save();
    }
}