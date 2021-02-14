using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new() //buraya herhangi bir class gelmesin, DbContext classı gelsin 
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using bloğunun işi bitince yaratılan class'ı iptal eder.
            //Garbage collector hemen devreye girer
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //referansı yakala
                addedEntity.State = EntityState.Added; //eklenecek bir nesnedir
                context.SaveChanges(); //ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                try
                {
                    var deletedEntity = context.Entry(entity); //referansı yakala
                    deletedEntity.State = EntityState.Deleted; //silinecek bir nesnedir
                    context.SaveChanges(); //sil
                }
                catch (Exception)
                {
                    Console.WriteLine("Id bulunamadı");
                }
               
            }
        }
        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity); //referansı yakala
                updatedEntity.State = EntityState.Modified; //update edilecek bir nesnedir
                context.SaveChanges(); //update et
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() //Null ise bu çalışır
                    : context.Set<TEntity>().Where(filter).ToList(); //Null değilse bu çalışır
            }
        }
    }
}
