using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeevAtend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeevAtend.Infrastructure.Data.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetWaitingAppointments();
    Task<Appointment> GetAppointmentById(Guid id);
    Task<Appointment> GetLastFinishedAppointment();
    Task CreateAppointment(Appointment appointment);
    Task UpdateAppointment(Appointment appointment);
}

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetWaitingAppointments()
    {
        return await _context.Appointments
            .Where(a => a.Status == AppointmentStatus.Waiting)
            .OrderBy(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<Appointment> GetAppointmentById(Guid id)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Appointment> GetLastFinishedAppointment()
    {
        return await _context.Appointments
            .Where(a => a.Status == AppointmentStatus.Finished)
            .OrderByDescending(a => a.FinishedAt)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAppointment(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAppointment(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }
}
