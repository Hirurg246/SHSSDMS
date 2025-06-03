using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class Lock
{
    public int Id { get; set; }

    public int RoomFromId { get; set; }

    public int RoomToId { get; set; }

    public int Status { get; set; }

    public virtual ICollection<LockUnlock> LockUnlocks { get; set; } = new List<LockUnlock>();

    public virtual Room RoomFrom { get; set; } = null!;

    public virtual Room RoomTo { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
