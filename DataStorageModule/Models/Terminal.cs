using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class Terminal
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int Status { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<TerminalAccess> TerminalAccesses { get; set; } = new List<TerminalAccess>();
}
