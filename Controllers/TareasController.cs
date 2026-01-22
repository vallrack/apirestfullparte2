using Microsoft.AspNetCore.Mvc;
using GestionTareasApi.Models;

namespace GestionTareasApi.Controllers
{
    [ApiController]
    [Route("api/tareas")] // La dirección base será http://localhost:5063/api/tareas
    public class TareasController : ControllerBase
    {
        // Creamos una lista en memoria para guardar las tareas mientras el programa corra
        private static List<TaskModel> _tareas = new List<TaskModel>();
        private static int _nextId = 1; // Contador para asignar IDs automáticamente

        // --- MÉTODOS PARA RECLAMAR DATOS (GET) ---

        [HttpGet] // Trae toda la lista
        public IActionResult Listar()
        {
            return Ok(new
            {
                mensaje = "Datos reclamados con éxito",
                datos = _tareas
            });
        }

        [HttpGet("buscar")] // Busca tareas por una palabra clave
        public IActionResult Buscar([FromQuery] string termino)
        {
            // Buscamos en la lista si el título contiene lo que escribió el usuario
            var resultados = _tareas.Where(t =>
                t.Title.Contains(termino ?? "", StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(new
            {
                mensaje = $"Búsqueda terminada. Encontré {resultados.Count} coincidencias.",
                datos = resultados
            });
        }

        // --- MÉTODO PARA ENVIAR DATOS (POST) ---

        [HttpPost]
        public IActionResult Crear([FromBody] TaskModel nuevaTarea)
        {
            // Le asignamos un número de ID y lo sumamos a la lista
            nuevaTarea.Id = _nextId++;
            _tareas.Add(nuevaTarea);

            // Respondemos que todo salió bien y mostramos qué se guardó
            return CreatedAtAction(nameof(Listar), new { id = nuevaTarea.Id }, new
            {
                mensaje = "Dato enviado con éxito",
                datoEnviado = nuevaTarea
            });
        }

        // --- MÉTODOS PARA ACTUALIZAR (PUT Y PATCH) ---

        [HttpPut("{id}")] // Cambia toda la información de una tarea
        public IActionResult Editar(int id, [FromBody] TaskModel actualizada)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound(new { mensaje = "Esa tarea no existe, no la puedo editar" });

            tarea.Title = actualizada.Title;
            tarea.Description = actualizada.Description;

            return Ok(new { mensaje = "Dato actualizado con éxito", dato = tarea });
        }

        [HttpPatch("{id}/completar")] // Solo cambia el estado a completado (true)
        public IActionResult Completar(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound(new { mensaje = "ID no encontrado" });

            tarea.IsCompleted = true;
            return Ok(new { mensaje = "¡Tarea terminada!", dato = tarea });
        }

        [HttpPatch("{id}/pendiente")] // Vuelve la tarea a pendiente (false)
        public IActionResult MarcarPendiente(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound(new { mensaje = "ID no encontrado" });

            tarea.IsCompleted = false;
            return Ok(new { mensaje = "La tarea ahora está pendiente de nuevo", dato = tarea });
        }

        // --- MÉTODO PARA BORRAR (DELETE) ---

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return NotFound(new { mensaje = "No encontré qué borrar" });

            _tareas.Remove(tarea);
            return Ok(new { mensaje = "Dato eliminado con éxito", idEliminado = id });
        }
    }
}