using System;
using System.Collections.Generic;

namespace DataService;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public decimal? Rating1 { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int? DeletedBy { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
