var builder = WebApplication.CreateBuilder(args);

// REGISTRO: Agrega el servicio de controladores al contenedor de dependencias
builder.Services.AddControllers();

var app = builder.Build();

// MIDDLEWARE: Habilita el mapeo de las rutas definidas en los controladores
// Sin esta línea, las peticiones POST/GET/PUT darán error 404.
app.MapControllers();

app.Run();