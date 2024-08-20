namespace Repository
{
    public interface IRepository<T>
    {
        T Save(T t);

        T Update(T t);

        List<T> List();

        void Delete(T t);

        T GetById(int Id);
    }
}