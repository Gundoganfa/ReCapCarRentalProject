using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository <T> where T:class, IEntity, new()
    {
        //Generic Constraint
        //class: referans tip
        //IEntity : IEntity veya IEntity implemente eden bir nesne olabilir
        //new() : new'lenebilir olmalı (IEntity'nin kendisi new'lenemez, onu implemente eden classlar new'lenebilir)
        //new'lenebilmek demek: gelen tipin non-abstract, bu projede concrete bir tip olması gerektiğini ifade eder.

        // how to use expressions : https://www.youtube.com/watch?v=sLQBUfbaAqw 
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
