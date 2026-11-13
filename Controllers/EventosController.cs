using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR02.Models;
using DSW_3P_PR02.Services;

namespace DSW_3P_PR02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _service;

    public EventosController(IEventoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Evento>> ObtenerTodos()
    {
        var eventos = _service.ObtenerTodos();
        return Ok(eventos);
    }
    

    [HttpGet("{id}")]
    public ActionResult<Evento> ObtenerPorId(int id)
    {
        var evento = _service.ObtenerPorId(id);
        if (evento == null)
        {
            return NotFound(new { mensaje = $"No se encontró el evento con ID {id}." });
        }

        return Ok(evento);
    }

    [HttpPost]
    public ActionResult<Evento> Crear([FromBody] Evento evento)
    {
        try
        {
            var creada = _service.Crear(evento);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, [FromBody] Evento evento)
    {
        try
        {
            var actualizada = _service.Actualizar(id, evento);
            if (!actualizada)
            {
                return NotFound(new { mensaje = $"No se encontró el evento con ID {id} para actualizar." });
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var eliminada = _service.Eliminar(id);
        if (!eliminada)
        {
            return NotFound(new { mensaje = $"No se encontró el evento con ID {id} para eliminar." });
        }

        return NoContent();
    }
}


