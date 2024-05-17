namespace Domain.Dtos.Movie;

public class MovieResponse
{
    public MovieResponse(int page, List<MovieResultAPiTmdb> results)
    {
        Page = page;
        Results = results;
    }

    public int Page { get; set; }
    public List<MovieResultAPiTmdb> Results { get; set; }
}