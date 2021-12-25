namespace ContentManagement.Dtos.Artist;

using ContentManagement.Dtos.Shared;

public class ArtistParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}