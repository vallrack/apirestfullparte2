# Gestión de Tareas API - Entrega Final

API REST desarrollada con ASP.NET Core para la gestión de actividades.

## Ejecución
1. Abrir terminal en la raíz del proyecto.
2. Comando: `dotnet run`.
3. URL Base: `http://localhost:5063`.

## Validación con Postman
- **POST `/api/tareas`**: Crea una tarea (enviar JSON en el Body).
- **GET `/api/tareas`**: Lista las tareas creadas.
- **PUT `/api/tareas/1`**: Actualiza la tarea con ID 1.
- **PATCH `/api/tareas/1/completar`**: Marca la tarea como completada.
- **DELETE `/api/tareas/1`**: Elimina la tarea.

*Nota: Todas las peticiones deben incluir el header 'Content-Type: application/json'.*