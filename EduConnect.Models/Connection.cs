using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Connection
{
    public string ConnectionId { get; set; } = null!;

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
