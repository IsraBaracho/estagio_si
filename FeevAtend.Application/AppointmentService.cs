using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeevAtend.Domain.Entities;
using FeevAtend.Domain.Repositories;
using FeevAtend.Application.DTOs;

namespace FeevAtend.Application.Services
{

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetWaitingAppointments();
    Task<AppointmentDto> CreateAppointment(CreateAppointmentDto dto);
    Task CallNextAppointment(Guid id, string attendantId);
    Task FinishAppointment(Guid id);
    Task<TimeSpan> GetEstimatedWaitTime();
}

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppointmentDto>> GetWaitingAppointments()
    {
        var appointments = await _repository.GetWaitingAppointments();
        return appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            Name = a.Name,
            RegistrationNumber = a.RegistrationNumber,
            CreatedAt = a.CreatedAt,
            CalledAt = a.CalledAt,
            FinishedAt = a.FinishedAt,
            Status = a.Status,
            AttendantName = a.AttendantId,
            WaitingTime = a.WaitingTime,
            ServiceTime = a.ServiceTime
        });
    }

    public async Task<AppointmentDto> CreateAppointment(CreateAppointmentDto dto)
    {
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            RegistrationNumber = dto.RegistrationNumber,
            CreatedAt = DateTime.Now,
            Status = AppointmentStatus.Waiting
        };

        await _repository.CreateAppointment(appointment);
        return new AppointmentDto
        {
            Id = appointment.Id,
            Name = appointment.Name,
            RegistrationNumber = appointment.RegistrationNumber,
            CreatedAt = appointment.CreatedAt,
            Status = appointment.Status,
            WaitingTime = appointment.WaitingTime
        };
    }

    public async Task CallNextAppointment(Guid id, string attendantId)
    {
        var appointment = await _repository.GetAppointmentById(id);
        if (appointment == null || appointment.Status != AppointmentStatus.Waiting)
            throw new InvalidOperationException("Appointment not found or invalid status");

        appointment.CalledAt = DateTime.Now;
        appointment.Status = AppointmentStatus.InProgress;
        appointment.AttendantId = attendantId;
        await _repository.UpdateAppointment(appointment);
    }

    public async Task FinishAppointment(Guid id)
    {
        var appointment = await _repository.GetAppointmentById(id);
        if (appointment == null || appointment.Status != AppointmentStatus.InProgress)
            throw new InvalidOperationException("Appointment not found or invalid status");

        appointment.FinishedAt = DateTime.Now;
        appointment.Status = AppointmentStatus.Finished;
        await _repository.UpdateAppointment(appointment);
    }

    public async Task<TimeSpan> GetEstimatedWaitTime()
    {
        var waitingAppointments = await _repository.GetWaitingAppointments();
        if (!waitingAppointments.Any()) return TimeSpan.Zero;

        var lastAppointment = await _repository.GetLastFinishedAppointment();
        if (lastAppointment == null) return TimeSpan.Zero;

        var averageWaitTime = lastAppointment.WaitingTime;
        return averageWaitTime * waitingAppointments.Count();
    }
}
}