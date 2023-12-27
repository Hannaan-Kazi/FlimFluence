using System;
using System.Collections.Generic;

namespace DataService;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int? Deletedby { get; set; }

    public string? HashedPassword { get; set; }

    public string? Salt { get; set; }
}
