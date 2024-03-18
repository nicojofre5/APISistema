using APIsistemaGestion.DTO;
using APIsistemaGestion.Mapper;
using APIsistemaGestion.Models;
using APIsistemaGestion.Excepciones;
using APIsistemaGestion.Database;
using Microsoft.EntityFrameworkCore;


namespace APIsistemaGestion.Service
{
    public class UsuarioService
    {
        private CoderContext context;
        public UsuarioService(CoderContext dataBaseContext)
        {

            this.context = dataBaseContext;
        }
        public List<UsuarioDTO> ObtenerListaDeUsuarios()
        {
            try
            {
                var usuariosDTO = this.context.Usuarios.Select(usuario => UsuarioMapper.MapearUsuarioAUsuarioDTO(usuario)).ToList();
                return usuariosDTO;

            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener la lista de usuarios. Detalle: {ex.Message}");
            }


        }

        public UsuarioDTO? ObtenerUsuarioPorID(int id)
        {

            try
            {
                Usuario? usuarioBuscado = this.context.Usuarios.Where(u => u.Id == id).FirstOrDefault();



                if (usuarioBuscado is not null)
                {
                    var usuarioDTO = UsuarioMapper.MapearUsuarioAUsuarioDTO(usuarioBuscado);
                    return usuarioDTO;
                }

                //return null;

                throw new Excepciongral($"usuario con id: {id} no existe.", 404);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo obtener el usuario . Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public UsuarioDTO? ObtenerUsuarioPorNombre(string nombre)
        {
            try
            {

                Usuario? usuarioBuscado = this.context.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

                if (usuarioBuscado is not null)
                {
                    var usuarioDTO = UsuarioMapper.MapearUsuarioAUsuarioDTO(usuarioBuscado);
                    return usuarioDTO;
                }

                //return null;

                throw new Excepciongral($"usuario con Nombre: {nombre} no existe.", 404);
            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo obtener el usuario. Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public UsuarioDTO? ObtenerUsuarioPorUserPass(string usuario, string password)
        {

            try
            {

                Usuario? usuarioBuscado = this.context.Usuarios.Where(u => u.NombreUsuario == usuario && u.Contraseña == password).FirstOrDefault();

                if (usuarioBuscado is not null)
                {

                    var usuarioDTO = UsuarioMapper.MapearUsuarioAUsuarioDTO(usuarioBuscado);
                    return usuarioDTO;
                }

                //return null;
                throw new Excepciongral($"usuario o contraseña invalida.", 404);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo iniciar sesion. Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool AgregarUsuario(UsuarioDTO usuarioDTO)
        {

            try
            {
                Usuario? usuarioBuscado = this.context.Usuarios.Where(u => u.NombreUsuario == usuarioDTO.NombreUsuario).FirstOrDefault();
                if (usuarioBuscado is null)
                {
                    var usuario = UsuarioMapper.MapearUsuarioDTOAUsuario(usuarioDTO);
                    if (usuarioDTO is not null)
                    {
                        this.context.Usuarios.Add(usuario);
                        this.context.SaveChanges();
                        return true;
                    }


                    return false;

                    throw new Excepciongral($"Formato de Usuario invalido.", 400);
                }


                throw new Excepciongral($"Usuario con nombre de usuario :{usuarioBuscado.NombreUsuario} existente", 409);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo agregar el usuario. Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool EliminarUsuario(int idUsuario)
        {
            try
            {
                Usuario? usuario = this.context.Usuarios.Include(u => u.Venta).ThenInclude(v => v.ProductoVendidos).Include(u => u.Productos).ThenInclude(p => p.ProductoVendidos).Where(u => u.Id == idUsuario).FirstOrDefault();

                if (usuario is not null)
                {
                    this.context.Usuarios.Remove(usuario);
                    this.context.SaveChanges();
                    return true;
                }

                //return false;

                throw new Excepciongral($"Usuario con id: {idUsuario} no encontrado.", 404);


            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo eliminar el usuario. Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool ActualizarUsuarioPorId(UsuarioDTO usuarioDTO)
        {
            try
            {

                if (usuarioDTO is not null)
                {

                    Usuario? usuarioBuscado = this.context.Usuarios.Where(u => u.Id == usuarioDTO.Id).FirstOrDefault();


                    if (usuarioBuscado is not null)
                    {

                        usuarioBuscado.Nombre = usuarioDTO.Nombre;
                        usuarioBuscado.NombreUsuario = usuarioDTO.NombreUsuario;
                        usuarioBuscado.Apellido = usuarioDTO.Apellido;
                        usuarioBuscado.Mail = usuarioDTO.Mail;
                        usuarioBuscado.Contraseña = usuarioDTO.Contraseña;


                        this.context.Usuarios.Update(usuarioBuscado);
                        this.context.SaveChanges();

                        return true;
                    }
                    //return false;
                    throw new Excepciongral($"Usuario con id:{usuarioDTO.Id}, no existe", 404);
                }
                //return false;
                throw new Excepciongral($"No se paso un objeto usuario valido", 400);


            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo actualizar el usuario. Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

    }
}
