using System;
using System.Collections.Generic;

namespace DataService;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PictureUrl { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int? Deletedby { get; set; }

    public string? Salt { get; set; }

    public string? HashedPassword { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<WatchLater> WatchLaters { get; set; } = new List<WatchLater>();

    public virtual ICollection<Watched> Watcheds { get; set; } = new List<Watched>();
}
