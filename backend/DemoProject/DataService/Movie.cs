using System;
using System.Collections.Generic;

namespace DataService;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? Title { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Summary { get; set; }

    public string? Genre { get; set; }

    public string? PosterUrl { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int? DeletedBy { get; set; }

    public decimal? Ratings { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Rating> RatingsNavigation { get; set; } = new List<Rating>();
}
