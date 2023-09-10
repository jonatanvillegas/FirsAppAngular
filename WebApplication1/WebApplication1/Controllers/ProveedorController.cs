using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private IConfiguration _Config;

        public ProveedorController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> GetAllProveedores()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var oCliente = conexion.Query<Proveedor>("SP_ObtenerTodosLosProveedores", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<List<Proveedor>>> GetProveedorbyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Proveedor>("SP_ObtenerProveedorPorID  ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPost]
        public async Task<ActionResult<List<Proveedor>>> CreateN_Proveedor(Proveedor cl)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Nombre", cl.Nombre);
            parametro.Add("@Direccion", cl.Direccion);
            parametro.Add("@Telefono", cl.Telefono);
            var oCliente = conexion.Query<Proveedor>("SP_InsertarProveedor ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPut]
        public async Task<ActionResult<List<Proveedor>>> UpdateProveedor(Proveedor cl)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", cl.Id);
            parametro.Add("@Nombre", cl.Nombre);
            parametro.Add("@Direccion", cl.Direccion);
            parametro.Add("@Telefono", cl.Telefono);
            var oCliente = conexion.Query<Proveedor>("SP_ActualizarProveedor ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Proveedor>>> DeleteProeedorbyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Proveedor>(" SP_EliminarProveedor", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }
    }
}
