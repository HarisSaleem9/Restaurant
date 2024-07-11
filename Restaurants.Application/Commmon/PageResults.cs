namespace Restaurants.Application.Commmon;

public class PageResults<T>
{
    public PageResults(IEnumerable<T> items,int totalcount,int pageSize , int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalcount;
        TotalPages =(int)Math.Ceiling(totalcount/(double)pageSize);
        ItemFrom = pageSize * (pageNumber - 1);
        ItemTo = ItemFrom + pageSize - 1;
    }
    public IEnumerable<T> Items { get; set; }
    public int TotalItemsCount { get; set; }
    public int TotalPages { get; set; }
    public int ItemFrom { get; set; }
    public int ItemTo { get; set; }

}
