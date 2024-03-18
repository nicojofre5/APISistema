using APIsistemaGestion.Models;
using APIsistemaGestion.DTO;

namespace APIsistemaGestion.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario MapearUsuarioDTOAUsuario(UsuarioDTO usuarioDTO)
        {

            Usuario usuario = new Usuario();

            usuario.Id = usuarioDTO.Id;
            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Apellido = usuarioDTO.Apellido;
            usuario.NombreUsuario = usuarioDTO.NombreUsuario;
            usuario.Contraseña = usuarioDTO.Contraseña;
            usuario.Mail = usuarioDTO.Mail;

            return usuario;
        }

        public static UsuarioDTO MapearUsuarioAUsuarioDTO(Usuario usuario)
        {

            UsuarioDTO usuarioDTO = new UsuarioDTO();

            usuarioDTO.Id = usuario.Id;
            usuarioDTO.Nombre = usuario.Nombre;
            usuarioDTO.Apellido = usuario.Apellido;
            usuarioDTO.NombreUsuario = usuario.NombreUsuario;
            usuarioDTO.Contraseña = usuario.Contraseña;
            usuarioDTO.Mail = usuario.Mail;

            return usuarioDTO;
        }
    }
}
