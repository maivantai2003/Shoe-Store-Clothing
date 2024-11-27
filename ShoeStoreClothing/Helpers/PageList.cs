namespace ShoeStoreClothing.Helpers
{
    public class PageList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public PageList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count * 1.0d / pageSize);
            AddRange(items);
        }
        public static PageList<T> Create(IQueryable<T> sources, int pageSize, int pageIndex)
        {
            var count = sources.Count();
            var items = sources.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageSize, pageIndex);
        }
    }
}
