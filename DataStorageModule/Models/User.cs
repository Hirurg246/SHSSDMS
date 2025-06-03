using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public byte[] PasswodHash { get; set; } = null!;

    public int Role { get; set; }

    public virtual Person IdNavigation { get; set; } = null!;

    public virtual ICollection<LockUnlock> LockUnlocks { get; set; } = new List<LockUnlock>();

    public virtual ICollection<TerminalAccess> TerminalAccesses { get; set; } = new List<TerminalAccess>();

    public virtual ICollection<Lock> Locks { get; set; } = new List<Lock>();
}
