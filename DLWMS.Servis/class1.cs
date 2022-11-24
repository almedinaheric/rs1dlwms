using DLWMS.Core;
using DLWMS.Repository;

namespace DLWMS.Servis
{
    //klase servisnog sloja obicno sluze za poslovnu logiku
    //npr na dlwms sistemu poslovna logika su pravila poslovanja npr. oranicenja npr da se ne moze prijavit ispit koji je uslovljen nekim koji nsmo polozili
    //logika oko desavanja sta ce se desiti kada se obavi neka akcija

    public interface IBaseService<TEntity,TKey>
    {
        void Save(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll();
    }

    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey>
    {
        private readonly IRepository<TEntity, TKey> repository;
        public BaseService(IRepository<TEntity, TKey> repository)
        {
            //cim se kreira objekat tipa student servis da se odmah injecta neko ko zna komunicirati sa repository -> ot je klasa Irepository
            this.repository = repository;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public TEntity GetById(TKey key)
        {
            return repository.GetById(key);
        }

        public void Save(TEntity entity)
        {
            repository.Save(entity);
        }

        public void Update(TEntity entity)
        {
            repository.Update(entity);
        }
    }

    public interface IStudentServis:IBaseService<Student,int>
    {
        IEnumerable<Student> GetByGodina(int godina);
    }

    public class StudentService : IBaseService<Student, int>, IStudentServis
    {
        public StudentService(IRepository<Student,int>, Repository):base(repository)
        {

        }
    }
}