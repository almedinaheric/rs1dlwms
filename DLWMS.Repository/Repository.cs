using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//repository kalsa osigurava osnovne funkcije 
//osigura insert update delete, osnovne CRUD operacije za sve tipove
//treba da bude template klasa

namespace DLWMS.Repository
{   
    //interfejs ima ulogu da nesto nametne da garantuje da nesto postoji na nekom tipu
    public interface IRepository<TEntity, TKey>
    {
        void Save(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll();
        //zasto je najekstremnija metoda? zbog getall jer se povlace zapisi iz velikih tabela (treba biti neko ogranicenje, npr prvih 100 zapisa)
        //sa interfejsom smo dobili set metoda koje zelimo da nam budu garantovane za svaki tip odataka
    }

    //Dependency injection; kad se kreia repositori odmah posalji dbcontext, on sa automatski injecta, umetne na mejsto gdje treba

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>where TEntity:class //TEntity mora biti klasa, zasto? Je npr ozemo rec int, string... a to nisu poolja za tabelu nego treba neka klasa u bazi
    {
        private readonly DLWMSDBContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(DLWMSDBContext DBContext)
        {
            dbContext = DBContext;
            dbSet = dbContext.Set<TEntity>();
            //u dbcontekstu, set sa kojim se radi
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
            //vrati kao listu, kompletan dbSet, set studenta, predmeta...
        }

        public TEntity GetById(TKey key)
        {
            return dbSet.Find(key);
        }

        public void Save(TEntity entity)
        {
            //dbContext.Studenti.Add(entity);   //ovako bi dbcontextu
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            dbContext.SaveChanges();
        }

        //repository klasa koja ce raditi sa svim tipovima podataka i za svaki tip osigurati metode, getall, save, update, getbyid
    }
}
