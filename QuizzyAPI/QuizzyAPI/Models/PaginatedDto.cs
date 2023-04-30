namespace QuizzyAPI.Models; 

public class PaginatedDto<T> where T : class {
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public long ItemCount { get; set; }
    
    public IEnumerable<T> Data { get; set; }
    
    public string? PreviousUrl { get; set; }
    public string? NextUrl { get; set; }

    public PaginatedDto(int pageIndex, int pageSize, int pageCount, long itemCount, IEnumerable<T> data, string? prevUrl, string? nextUrl) {
        PageIndex = pageIndex;
        PageSize = pageSize;
        PageCount = pageCount;
        ItemCount = itemCount;
        Data = data;

        PreviousUrl = prevUrl;
        NextUrl = nextUrl;
    }
}