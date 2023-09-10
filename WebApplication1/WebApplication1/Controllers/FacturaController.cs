using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Globalization;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IConfiguration _Config;

        public FacturaController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Factura>>> GetAllFacturas()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var oCliente = conexion.Query<Factura>("SP_ObtenerTodasLasFacturas", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<List<Factura>>> GetFacturabyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Factura>("sp_ObtenerFacturaPorID ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPost]
        public async Task<ActionResult<List<Factura>>> CreateN_Factura(Factura fac)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();

            // Convertir la propiedad "Fecha" a tipo Date
            if (DateTime.TryParseExact(fac.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                fac.Fecha = fechaConvertida.ToString("yyyy-MM-dd"); // Ajustar el formato de fecha si es necesario
            }
            else
            {
                return BadRequest("Formato de fecha inválido.");
            }

            var parametro = new DynamicParameters();
            parametro.Add("@Cliente_id", fac.Cliente_Id);
            parametro.Add("@Empleado_Id", fac.Empleado_Id);
            parametro.Add("@Pedido_Cliente_Id", fac.Pedido_Cliente_Id);
            parametro.Add("@Fecha", fac.Fecha);
            parametro.Add("@Total_Pagar", fac.Total_Pagar);
            var oCliente = conexion.Query<Factura>("SP_InsertarFactura", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPut]
        public async Task<ActionResult<List<Factura>>> UpdateFactura(Factura fac)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();

            // Convertir la propiedad "Fecha" a tipo DateTime
            if (DateTime.TryParseExact(fac.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                fac.Fecha = fechaConvertida.ToString("yyyy-MM-dd"); // Ajustar el formato de fecha si es necesario
            }
            else
            {
                return BadRequest("Formato de fecha inválido.");
            }

            var parametro = new DynamicParameters();
            parametro.Add("@Id", fac.Id);
            parametro.Add("@Cliente_id", fac.Cliente_Id);
            parametro.Add("@Empleado_Id", fac.Empleado_Id);
            parametro.Add("@Pedido_Cliente_Id", fac.Pedido_Cliente_Id);
            parametro.Add("@Fecha", fac.Fecha);
            parametro.Add("@Total_Pagar", fac.Total_Pagar);
            var oCliente = conexion.Query<Factura>("SP_ActualizarFactura", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Factura>>> DeleteFactura(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Factura>("SP_EliminarFactura", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }
    }
}
