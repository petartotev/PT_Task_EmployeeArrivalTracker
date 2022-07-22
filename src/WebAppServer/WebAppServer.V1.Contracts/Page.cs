namespace WebAppServer.V1.Contracts;

public class Page<TModel>
{
    public Page()
    {
    }

    public Page(IEnumerable<TModel> items, int totalItems, int skip, int take)
    {
        Items = items.ToList();
        ItemsPerPage = take;
        TotalItems = totalItems;
        TotalPages = (totalItems / take) + (totalItems % take != 0 ? 1 : 0);
        CurrentPage = Math.Min(TotalPages, Math.Max(1, skip / take + (skip % take == 0 ? 1 : 0)));
    }

    public int CurrentPage { get; set; }

    public int ItemsPerPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalItems { get; set; }

    public IEnumerable<TModel> Items { get; set; }
}
