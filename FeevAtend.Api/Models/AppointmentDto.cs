using System;
using FeevAtend.Domain.Entities;

namespace FeevAtend.Api.Models;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string RegistrationNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CalledAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public AppointmentStatus Status { get; set; }
    public string AttendantName { get; set; }
    public TimeSpan WaitingTime { get; set; }
    public TimeSpan ServiceTime { get; set; }
}

public class CreateAppointmentDto
{
    public string Name { get; set; }
    public string RegistrationNumber { get; set; }
}

public class UpdateAppointmentStatusDto
{
    public AppointmentStatus Status { get; set; }
    public string AttendantId { get; set; }
}
