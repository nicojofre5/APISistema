using APIsistemaGestion.Models;
using APIsistemaGestion.Excepciones;
using APIsistemaGestion.DTO;
using APIsistemaGestion.Mapper;
using APIsistemaGestion.Database;
using Microsoft.EntityFrameworkCore;
namespace APIsistemaGestion.Service
{
    public class ProductoService
    {
        private CoderContext context;

        public ProductoService(CoderContext dataBaseContext)
        {

            this.context = dataBaseContext;

        }

        public List<ProductoDTO> ObtenerListaDeProductos()
        {
            try
            {
                var productosDTO = this.context.Productos.Select(producto => ProductoMapper.MapearProductoAProductoDTO(producto)).ToList();

                return productosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"no se obtiene la lista de productos, el error es: {ex.Message}");
            }

        }


        public List<ProductoDTO>? ObtenerProductosPorIdDeUsuario(int idUsuario)
        {
            try
            {
                var usuario = this.context.Usuarios.Any(u => u.Id == idUsuario);
                var listaProductosDTO = this.context.Productos.Where(p => p.IdUsuario == idUsuario).Select(p => ProductoMapper.MapearProductoAProductoDTO(p)).ToList();

                if (usuario)
                {
                    if (listaProductosDTO.Count != 0)
                    {
                        return listaProductosDTO;
                    }

                    //return null;

                    throw new Excepciongral($"El usuario con id: {idUsuario}, no posee productos asociados a el.", 404);
                }

                throw new Excepciongral($"Usuario con id : {idUsuario} no encontrado", 404);


            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se encontraron productos para el usuario, el motivo es: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProductoDTO? ObtenerProductoPorID(int id)
        {

            try
            {
                Producto? productoBuscado = this.context.Productos.Where(u => u.Id == id).FirstOrDefault();

                if (productoBuscado is not null)
                {
                    var productoDTO = ProductoMapper.MapearProductoAProductoDTO(productoBuscado);
                    return productoDTO;
                }


                //return null;

                throw new Excepciongral($"Producto con id: {id} no encontrado", 404);
            }
            catch(Excepciongral ex)
            {
                throw new Excepciongral($"No se puede obtener el producto {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public bool AgregarProducto(ProductoDTO productoDto)
        {

            try
            {
                var producto = ProductoMapper.MapearProductoDTOAProducto(productoDto);
                if (producto is not null)
                {
                    Usuario? idUsuarioEncontrado = this.context.Usuarios.Where(u => u.Id == producto.IdUsuario).FirstOrDefault();

                    if (idUsuarioEncontrado is not null)
                    {
                        this.context.Productos.Add(producto);
                        this.context.SaveChanges();
                        return true;
                    }

                    throw new Excepciongral($"Usuario con id: {producto.IdUsuario} no encontrado.", 404);

                }

                //return false;
                throw new Excepciongral("El produto que ha ingresado no se puede mostrar", 400);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"Error al egregar producto, el error es: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool EliminarProducto(int id)
        {
            try
            {
                Producto? producto = this.context.Productos.Include(p => p.ProductoVendidos).Where(p => p.Id == id).FirstOrDefault();

                if (producto is not null)
                {
                    this.context.Productos.Remove(producto);
                    this.context.SaveChanges();
                    return true;
                }

                //return false;
                throw new Excepciongral($"producto con id: {id} no encontrado.", 404);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo eliminar producto: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool ActualizarProductoPorId(ProductoDTO productoDTO)
        {
            try
            {

                var productoBuscado = this.context.Productos.Where(p => p.Id == productoDTO.Id).FirstOrDefault();

                var usuarioBuscado = this.context.Usuarios.Where(u => u.Id == productoDTO.IdUsuario).FirstOrDefault();

                if (usuarioBuscado is null) throw new Excepciongral($"Usuario con id: {productoDTO.IdUsuario} no encontrado.", 404);


                if (productoBuscado is not null)
                {

                    var producto = ProductoMapper.MapearProductoDTOAProducto(productoDTO, productoBuscado);

                    this.context.Productos.Update(producto);
                    this.context.SaveChanges();

                    return true;
                }

                //return false;
                throw new Excepciongral($"Producto con id: {productoDTO.Id} no encontrado.", 404);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"Problema par amodifciar producto {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public bool ActualizarStockDeProducto(ProductoDTO productoDTO)
        {
            try
            {

                var productoBuscado = this.context.Productos.Where(p => p.Id == productoDTO.Id).FirstOrDefault();


                if (productoBuscado is not null)
                {
                    productoBuscado.Stock = productoDTO.Stock;
                    this.context.Productos.Update(productoBuscado);
                    this.context.SaveChanges();

                    return true;
                }

                //return false;

                throw new Excepciongral($"El producto con id: {productoDTO.Id} no encontrado.", 404);


            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"Problema para actualizar: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
