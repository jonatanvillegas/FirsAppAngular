using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly IConfiguration _Config;
        private readonly IConnectionService _connectionService;

        public ConnectionController(IConfiguration configuration, IConnectionService connectionService)
        {
            _Config = configuration;
            _connectionService = connectionService;
        }

        [HttpPost("change-connection")]
        public IActionResult ChangeConnectionString([FromBody] Models.ConnectionModel connectionModel)
        {
            try
            {
                // Obtener la cadena de conexión actual
                string currentConnectionString = _connectionService.GetConnectionString();

                // Construir la nueva cadena de conexión con los nuevos parámetros
                string newConnectionString = $"{currentConnectionString};User Id={connectionModel.Username};Password={connectionModel.Password}";

                // Cambiar las credenciales de la cadena de conexión
                _connectionService.SetConnectionString(newConnectionString);

                return Ok("Cadena de conexión actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cambiar la cadena de conexión: {ex.Message}");
            }
        }
    }
}
