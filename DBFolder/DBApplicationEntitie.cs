using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ServerInformation;
using ServerInformation.ExceptionFolder;

namespace CommonLibrary
{
    public static class DBApplicationEntitie
    {
        public static bool HasAnyRelationships(this ApplicationEntitie db, Func<ObjectStateEntry, int, object> getValue)
        {
            db.ChangeTracker.DetectChanges();
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;

            return objectContext
                .ObjectStateManager
                .GetObjectStateEntries(EntityState.Added)
                .Where(e => e.IsRelationship)
                .Select(e => Tuple.Create(
                    objectContext.GetObjectByKey((EntityKey)getValue(e, 0)),
                    objectContext.GetObjectByKey((EntityKey)getValue(e, 1))))
                .Any() || objectContext
                       .ObjectStateManager
                       .GetObjectStateEntries(EntityState.Deleted)
                       .Any(e => e.IsRelationship);

            //                       .Select(e => Tuple.Create(
            //                                                 objectContext.GetObjectByKey((EntityKey) getValue(e, 0)),
            //                                                 objectContext.GetObjectByKey((EntityKey) getValue(e, 1)))).Any();
        }

        public static bool HasUnsavedChanges(this ApplicationEntitie db)
        {
            return db.ChangeTracker.Entries()
                .Any(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted) ||
                   db.HasAnyRelationships((e, i) => e.CurrentValues[i]);
        }

        public static void Remove(this ApplicationEntitie db, object entity)
        {
            try
            {
                if (db.Entry(entity).State == EntityState.Added)
                    db.Entry(entity).State = EntityState.Detached;
                else if (db.Entry(entity).State != EntityState.Deleted)
                    db.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception e)
            {
                DBException.WriteLog(e);
            }
        }
        public static bool CheckExist(this ApplicationEntitie db, object entity)
        {
            try
            {
                if (db.Entry(entity).State != EntityState.Detached)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static bool Any<T>(this ApplicationEntitie db, Func<T, bool> predicate) where T : class
        {
            return db.Set<T>()
                .Any(predicate) || db.Set<T>()
                    .Local.Any(predicate);
        }

        public static void Add(this ApplicationEntitie db, object entity)
        {
            try
            {
                if (db.Entry(entity).State != EntityState.Added)
                    db.Entry(entity).State = EntityState.Added;
            }
            catch (Exception e)
            {
                DBException.WriteLog(e);
            }
        }

        public static void Refresh(this ApplicationEntitie db, object instance)
        {
            db.Entry(instance).Reload();
        }

        public static void Refresh<T>(this ApplicationEntitie db) where T : class
        {
            foreach (var entity in db.Set<T>())
            {
                db.Entry(entity).State = EntityState.Detached;
            }
            db.Set<T>().Load();
        }

        public static IList<T> ToList<T>(this ApplicationEntitie db) where T : class
        {
            return db.Set<T>().ToList();
        }

        public static T[] ToArray<T>(this ApplicationEntitie db) where T : class
        {
            return db.Set<T>().ToArray();
        }

        public static IEnumerable<T> Where<T>(this ApplicationEntitie db, Func<T, bool> predicate) where T : class
        {
            return db.Set<T>()
                .Where(predicate)
                .Union(db.Set<T>().Local.Where(predicate));
        }

        public static IEnumerable<T> WhereLocal<T>(this ApplicationEntitie db, Func<T, bool> predicate) where T : class
        {
            return db.Set<T>().Local.Where(predicate);
        }
    }
}