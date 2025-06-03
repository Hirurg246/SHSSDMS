using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class Camera
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int Status { get; set; }

    public virtual Room IdNavigation { get; set; } = null!;

    public virtual ICollection<VideoRecording> VideoRecordings { get; set; } = new List<VideoRecording>();
}
