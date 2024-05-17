using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class JuntinMovie : BaseEntity
{
    public string Title { get; set; }
    public string UrlImage { get; set; }
    public string Description { get; set; }
    public int Views { get; set; }
    public bool IsWatchedEveryone { get; set; }
    public int TmdbId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid JuntinPlayId { get; set; }
    public JuntinPlay JuntinPlay { get; set; }
}