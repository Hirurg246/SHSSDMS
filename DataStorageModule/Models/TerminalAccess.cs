using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class TerminalAccess
{
    public int TerminalId { get; set; }

    public int UserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual Terminal Terminal { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
