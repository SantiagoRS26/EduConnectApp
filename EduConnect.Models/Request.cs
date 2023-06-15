using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Request
{
    public Guid RequestId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CollegeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Chat> ChatRequestId1Navigations { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatRequestId2Navigations { get; set; } = new List<Chat>();

    public virtual College? College { get; set; }

    public virtual User? User { get; set; }
}
