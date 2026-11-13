using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Services;

public interface IEventoService
{
    List<Evento> ObtenerTodos();
    Evento? ObtenerPorId(int id);
    Evento Crear(Evento evento);
    bool Actualizar(int id, Evento evento);
    bool Eliminar(int id);
}
