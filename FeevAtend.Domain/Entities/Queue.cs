using System;

namespace FeevAtend.Domain.Entities;

public class Queue
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CurrentPosition { get; set; }
    public int AverageWaitTime { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool IsActive { get; set; }
}
