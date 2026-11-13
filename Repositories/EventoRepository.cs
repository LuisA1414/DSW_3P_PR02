using DSW_3P_PR02.Models;
using System.Text.Json;

namespace DSW_3P_PR02.Repositories;

public class EventoJsonRepository : IEventoRepository
{

    private string _ruta;
    private JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public EventoJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "Data", "eventos.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta,"[]");
        }
    }

    private List<Evento> Leer()
    {
        string json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Evento>>(json) ?? new List<Evento>();
    }

    private void Guardar(List<Evento> datos)
    {
        string json = JsonSerializer.Serialize(datos, _opciones);
        File.WriteAllText(_ruta,json);
    }
    public List<Evento> ObtenerTodos()
    {
        return Leer();
    }

    public Evento? ObtenerPorId(int id)
    {
        return Leer().FirstOrDefault(m => m.Id == id);
    }

    public Evento Crear(Evento nuevo)
    {
        var datos = Leer();

        nuevo.Id = datos.Count == 0 ? 1 : datos.Max(m => m.Id) + 1;
        datos.Add(nuevo);
        Guardar(datos);

        return nuevo;
    }

    public bool Actualizar(int id, Evento actualizado)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(m => m.Id == id);

        if(existente == null)
            return false;

        existente.Nombre= actualizado.Nombre;
        existente.Categoria = actualizado.Categoria;
        existente.Fecha = actualizado.Fecha;
        existente.Lugar = actualizado.Lugar;
        existente.CupoMaximo = actualizado.CupoMaximo;
        existente.Activo = actualizado.Activo;

        Guardar(datos);

        return true;
    }

    public bool Eliminar(int id)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(m => m.Id == id);

        if(existente == null)
            return false;

        datos.Remove(existente);
        Guardar(datos);
        return true;
    }
}