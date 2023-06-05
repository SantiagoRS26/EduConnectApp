using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Match
{
    public Guid MatchId { get; set; }

    public Guid? RequestIdUser1 { get; set; }

    public Guid? RequestIdUser2 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual Request? RequestIdUser1Navigation { get; set; }

    public virtual Request? RequestIdUser2Navigation { get; set; }
}
