using System;
using System.Collections.Generic;

namespace DataStorageModule.Models;

public partial class VideoRecording
{
    public int CameraId { get; set; }

    public DateTime TimeStart { get; set; }

    public DateTime TimeEnd { get; set; }

    public virtual Camera Camera { get; set; } = null!;
}
