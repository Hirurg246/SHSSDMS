using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class LockUnlock
{
    public int LockId { get; set; }

    public int UnlockUserId { get; set; }

    public DateTime UnlockTime { get; set; }

    public DateTime? RelockTime { get; set; }

    public virtual Lock Lock { get; set; } = null!;

    public virtual User UnlockUser { get; set; } = null!;
}
