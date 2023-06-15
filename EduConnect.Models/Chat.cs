using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Chat
{
    public Guid ChatId { get; set; }

    public Guid? RequestId1 { get; set; }

    public Guid? RequestId2 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual Request? RequestId1Navigation { get; set; }

    public virtual Request? RequestId2Navigation { get; set; }
}
