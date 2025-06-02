using System;

namespace FeevAtend.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string RegistrationNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CalledAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public AppointmentStatus Status { get; set; }
    public string AttendantId { get; set; }
    public TimeSpan WaitingTime => CalledAt.HasValue ? CalledAt.Value - CreatedAt : TimeSpan.Zero;
    public TimeSpan ServiceTime => FinishedAt.HasValue && CalledAt.HasValue ? FinishedAt.Value - CalledAt.Value : TimeSpan.Zero;
}

public enum AppointmentStatus
{
    Waiting = 1,
    InProgress = 2,
    Finished = 3,
    Canceled = 4
}
