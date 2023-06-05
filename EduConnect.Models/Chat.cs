using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Chat
{
    public Guid ChatId { get; set; }

    public Guid? MatchId { get; set; }

    public string? Message { get; set; }

    public DateTime? SentDate { get; set; }

    public string? Sender { get; set; }

    public virtual Match? Match { get; set; }
}
