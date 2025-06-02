using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeevAtend.Api.Models;
using FeevAtend.Domain.Entities;

namespace FeevAtend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetWaitingAppointments()
    {
        var appointments = await _appointmentService.GetWaitingAppointments();
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto dto)
    {
        var appointment = await _appointmentService.CreateAppointment(dto);
        return CreatedAtAction(nameof(GetWaitingAppointments), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}/call")]
    public async Task<ActionResult> CallNextAppointment(Guid id, string attendantId)
    {
        await _appointmentService.CallNextAppointment(id, attendantId);
        return Ok();
    }

    [HttpPut("{id}/finish")]
    public async Task<ActionResult> FinishAppointment(Guid id)
    {
        await _appointmentService.FinishAppointment(id);
        return Ok();
    }

    [HttpGet("estimate")]
    public async Task<ActionResult<TimeSpan>> GetEstimatedWaitTime()
    {
        var waitTime = await _appointmentService.GetEstimatedWaitTime();
        return Ok(waitTime);
    }
}
