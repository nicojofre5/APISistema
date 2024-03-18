using APIsistemaGestion.DTO;
using APIsistemaGestion.Excepciones;
using APIsistemaGestion.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIsistemaGestion.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class UsuarioController : Controller
        {
            readonly private UsuarioService usuarioService;

            public UsuarioController(UsuarioService usuarioService)
            {
                this.usuarioService = usuarioService;
            }


            [HttpGet("listaDeUsuarios")]
            public List<UsuarioDTO> ObtenerListaDeUsuarios()
            {
                try
                {
                    var listaDeUsuarios = this.usuarioService.ObtenerListaDeUsuarios();
                    return listaDeUsuarios;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            [HttpGet("{nombreDeUsuario}")]
            public ActionResult<UsuarioDTO> ObtenerUsuarioPorNombre(string nombreDeUsuario)
            {
                try
                {
                    if (nombreDeUsuario is not null && nombreDeUsuario != string.Empty)
                    {
                        var usuarioObtenido = this.usuarioService.ObtenerUsuarioPorNombre(nombreDeUsuario);

                        if (usuarioObtenido is not null)
                        {
                            return Ok(usuarioObtenido);
                        }
                    }

                    return NotFound(new { mensaje = $"el Usuario con nombre {nombreDeUsuario} no fue encontrado", Status = "404" });

                }
                catch (Excepciongral ex)
                {
                    return StatusCode(ex.HttpStatusCode, new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            [HttpGet("{usuario}/{password}")]
            public ActionResult<UsuarioDTO> ObtenerUsuarioPorUserPass(string usuario, string password)
            {
                try
                {
                    if (usuario is not null && usuario != string.Empty)
                    {
                        var usuarioObtenido = this.usuarioService.ObtenerUsuarioPorUserPass(usuario, password);

                        if (usuarioObtenido is not null)
                        {
                            return Ok(usuarioObtenido);
                        }
                    }

                    return NotFound(new { mensaje = $"El usuario no se pudo logear", Status = "404" });
                }
                catch (Excepciongral ex)
                {
                    return StatusCode(ex.HttpStatusCode, new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

          

            [HttpPost]
            public IActionResult AgregarUsuario([FromBody] UsuarioDTO usuarioDTO)
            {
                try
                {
                    if (this.usuarioService.AgregarUsuario(usuarioDTO))
                    {

                        return this.Ok(new { message = usuarioDTO, status = "200", respuesta = "Usuario agregado correctamente" });
                    }
                    else
                    {
                        return this.Conflict(new { message = "No pudo agregar Usuario a la BD", status = "400" });
                    }
                }
                catch (Excepciongral ex)
                {
                    return StatusCode(ex.HttpStatusCode, new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            [HttpPut]
            public IActionResult ActualizarUsuarioPorId([FromBody] UsuarioDTO usuarioDTO)
            {
                try
                {

                    if (this.usuarioService.ActualizarUsuarioPorId(usuarioDTO))
                    {
                        return this.Ok(new { message = $"Usuario con ID: {usuarioDTO.Id} fue modificado", status = "200" });
                    }
                    else
                    {
                        return this.BadRequest(new { messge = $"El Usuario con ID: {usuarioDTO.Id} no pudo ser modificado", status = "400" });
                    }
                }
                catch (Excepciongral ex)
                {
                    return StatusCode(ex.HttpStatusCode, new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

          


        }
    }
