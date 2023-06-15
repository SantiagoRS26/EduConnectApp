using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class ChatMessage
{
    public Guid MessageId { get; set; }

    public Guid? ChatId { get; set; }

    public Guid? SenderId { get; set; }

    public string? Message { get; set; }

    public DateTime? SentDate { get; set; }

    public virtual Chat? Chat { get; set; }

    public virtual User? Sender { get; set; }
}
