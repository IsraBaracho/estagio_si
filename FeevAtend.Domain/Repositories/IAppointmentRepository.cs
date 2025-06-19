using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeevAtend.Domain.Entities;

namespace FeevAtend.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetWaitingAppointments();
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> GetById(Guid id);
Task<Appointment> GetAppointmentById(Guid id);
Task UpdateAppointment(Appointment appointment);
Task<Appointment> GetLastFinishedAppointment();
        Task Update(Appointment appointment);
    }
}
