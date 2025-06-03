using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class Room
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public int Number { get; set; }

    public virtual Camera? Camera { get; set; }

    public virtual ICollection<Lock> LockRoomFroms { get; set; } = new List<Lock>();

    public virtual ICollection<Lock> LockRoomTos { get; set; } = new List<Lock>();

    public virtual ICollection<Terminal> Terminals { get; set; } = new List<Terminal>();
}
