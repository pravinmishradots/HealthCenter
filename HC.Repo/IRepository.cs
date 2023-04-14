using HC.Core;
using HC.DataTable.Search;

namespace HC.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        void InsertGraph(TEntity entity);
        void Update(TEntity entity);
        TEntity Update(TEntity dbEntity, TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        void ChangeEntityState<T>(T entity, ObjectState state) where T : class;
        RepositoryQuery<TEntity> Query();
        void SafeAttach(TEntity entity, Func<TEntity, object> keyFn);
        //void ExecuteSqlCommand(string query);
        void SaveChanges();
        void UpdateCollection(List<TEntity> entityCollection);
        void InsertCollection(List<TEntity> entityCollection);
        void Dispose();
        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery);
        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery, out int totalCount);

    }
}
