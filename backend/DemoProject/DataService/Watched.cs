using System;
using System.Collections.Generic;

namespace DataService;

public partial class Watched
{
    public int WatchedId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public decimal? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int? Deletedby { get; set; }

    public virtual User? User { get; set; }
}
