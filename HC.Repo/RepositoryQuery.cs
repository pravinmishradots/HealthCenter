using System.Linq.Expressions;

namespace HC.Repo
{
    public sealed class RepositoryQuery<TEntity> where TEntity : class
    {
        private readonly List<Expression<Func<TEntity, object>>>
            _includeProperties;

        private readonly List<string>
            _includeStringProperties;

        private readonly Repository<TEntity> _repository;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> _orderByQuerable;
        private Func<IQueryable<TEntity>,
            IQueryable<TEntity>> _customOrderByQuerable;
        private int? _page;
        private int? _pageSize;

        public RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties =
                new List<Expression<Func<TEntity, object>>>();
            _includeStringProperties = new List<string>();
        }

        public RepositoryQuery<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public RepositoryQuery<TEntity> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<TEntity> CustomOrderBy(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
        {
            _customOrderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<TEntity> Include(
            Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public RepositoryQuery<TEntity> Include(
    string expression)
        {
            _includeStringProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetPage(
            int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter, includeProperties: _includeProperties, includeStringProperties: _includeStringProperties).Count();

            return _repository.Get(
                _filter,
                _orderByQuerable, _customOrderByQuerable, _includeProperties, _includeStringProperties, _page, _pageSize);
        }

        public IEnumerable<TEntity> Get()
        {
            return _repository.Get(
                _filter,
                _orderByQuerable, _customOrderByQuerable, _includeProperties, _includeStringProperties, _page, _pageSize);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _repository.Get(
                _filter,
                _orderByQuerable, _customOrderByQuerable, _includeProperties, _includeStringProperties, _page, _pageSize);
        }
    }
}
