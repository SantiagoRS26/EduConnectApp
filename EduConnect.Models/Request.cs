using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Request
{
    public Guid RequestId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CollegeId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual College? College { get; set; }

    public virtual ICollection<Match> MatchRequestIdUser1Navigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchRequestIdUser2Navigations { get; set; } = new List<Match>();

    public virtual User? User { get; set; }
}
