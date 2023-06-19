using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class History
{
    public Guid HistoryId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CollegeId { get; set; }

    public string? ChangeType { get; set; }

    public DateTime? ChangeDate { get; set; }

    public virtual College? College { get; set; }

    public virtual User? User { get; set; }
}
