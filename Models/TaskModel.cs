using System.ComponentModel.DataAnnotations; // Librería para validar datos

namespace GestionTareasApi.Models
{
    public class TaskModel
    {
        // El ID será como la cédula de la tarea, único e irrepetible
        public int Id { get; set; }

        // Con [Required] hacemos que el sistema no deje crear tareas sin nombre
        [Required(ErrorMessage = "¡Oye! No puedes dejar el título vacío.")]
        public string Title { get; set; } = string.Empty;

        // Una descripción breve de lo que hay que hacer
        public string Description { get; set; } = string.Empty;

        // Por defecto, toda tarea nueva empieza como "no completada" (false)
        public bool IsCompleted { get; set; } = false;
    }
}