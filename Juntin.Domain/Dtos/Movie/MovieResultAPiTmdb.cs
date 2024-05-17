namespace Domain.Dtos.Movie;

public record MovieResultAPiTmdb
{
    public MovieResultAPiTmdb(
        int id,
        bool adult,
        string backdropPath,
        List<int> genreIds,
        string originalLanguage,
        string original_Title,
        string overview,
        double popularity,
        string poster_Path,
        string releaseDate,
        string title,
        bool video,
        double voteAverage,
        int voteCount)
    {
        Id = id;
        Adult = adult;
        BackdropPath = backdropPath;
        GenreIds = genreIds;
        OriginalLanguage = originalLanguage;
        Original_Title = original_Title;
        Overview = overview;
        Popularity = popularity;
        Poster_Path = poster_Path;
        ReleaseDate = releaseDate;
        Title = title;
        Video = video;
        VoteAverage = voteAverage;
        VoteCount = voteCount;
    }

    public bool Adult { get; set; }
    public string BackdropPath { get; set; }
    public List<int> GenreIds { get; set; }
    public int Id { get; set; }
    public string OriginalLanguage { get; set; }
    public string Original_Title { get; set; }
    public string Overview { get; set; }
    public double Popularity { get; set; }
    public string Poster_Path { get; set; }
    public string ReleaseDate { get; set; }
    public string Title { get; set; }
    public bool Video { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
}