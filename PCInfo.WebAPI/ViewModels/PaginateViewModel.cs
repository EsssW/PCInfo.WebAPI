namespace PCInfo.WebAPI.ViewModels;

public class PaginateViewModel<T>
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public List<T> Items { get; set; } = new();
}