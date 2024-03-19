using APIsistemaGestion.Database;
using APIsistemaGestion.DTO;
using APIsistemaGestion.Models;
using APIsistemaGestion.Mapper;
using APIsistemaGestion.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace APIsistemaGestion.Service
{
    public class VentaService
    {
        private CoderContext context;
        private ProductoService productoService;
        private ProductoVendidoService productoVendidoService;
        private UsuarioService usuarioService;

        public VentaService(CoderContext dataBaseContext)
        {

            this.context = dataBaseContext;
            this.productoService = new ProductoService(dataBaseContext);
            this.productoVendidoService = new ProductoVendidoService(dataBaseContext);
            this.usuarioService = new UsuarioService(dataBaseContext);


        }

        public List<VentaDTO> ObtenerListaDeVentas()
        {
            try
            {
                var listaVentas = this.context.Venta.Select(venta => VentaMapper.MapearVentaAVentaDTO(venta)).ToList();
                return listaVentas;

            }
            catch (Exception ex)
            {
                throw new Exception($"No se obtiene la venta, con el mensaje: {ex.Message}");
            }

        }


        public List<VentaDTO> ObtenerVentasPorIdUsuario(int idUsuario)
        {

            try
            {
                var listaDeVentasDeUsuario = this.context.Venta.Where(v => v.IdUsuario == idUsuario).ToList();

                if (listaDeVentasDeUsuario is not null)
                {

                    List<VentaDTO> listaDeVentasDTO = new List<VentaDTO>();

                    foreach (var venta in listaDeVentasDeUsuario)
                    {


                        var ventaDTO = VentaMapper.MapearVentaAVentaDTO(venta);
                        listaDeVentasDTO.Add(ventaDTO);


                    }
                    if (listaDeVentasDTO is not null)
                    {
                        return listaDeVentasDTO;
                    }
                    else
                    {
                        throw new Excepciongral("el servidor ha fallado la carga", 500);
                    }

                }
                else
                {
                    throw new Excepciongral($"No se encontraron ventas asociadas a este Usuario id: {idUsuario}", 404);
                }

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se han podido obtener vventas, el mensaje es: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

     
        public VentaDTO? ObtenerVentaPorID(int id)
        {
            try
            {
                Ventum? ventaBuscada = this.context.Venta.Where(u => u.Id == id).FirstOrDefault();

                if (ventaBuscada is not null)
                {
                    var ventaDTO = VentaMapper.MapearVentaAVentaDTO(ventaBuscada);
                    return ventaDTO;
                }

                //return null;
                throw new Excepciongral($"Venta con id: {id} no encontrada", 404);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo obtener la venta, el error: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool AgregarVenta(VentaDTO ventaDTO)
        {


            try
            {
                if (ventaDTO is not null)
                {
                    Usuario? idUsuarioEncontrado = this.context.Usuarios.Where(u => u.Id == ventaDTO.IdUsuario).FirstOrDefault();


                    if (idUsuarioEncontrado is not null)
                    {
                        var venta = VentaMapper.MapearVentaDTOAVenta(ventaDTO);
                        this.context.Venta.Add(venta);
                        this.context.SaveChanges();
                        return true;
                    }
                    else
                    {
                    
                        throw new Excepciongral($"Usuario con id: {ventaDTO.IdUsuario} no encontrado.", 404);
                    }
                }

            
                throw new Excepciongral("Formato de venta invalida.", 400);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se ha podido agregar la venta, el error es: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool EliminarVenta(int id)
        {
            try
            {
                Ventum? venta = this.context.Venta.Include(v => v.ProductoVendidos).Where(v => v.Id == id).FirstOrDefault();

                if (venta is not null)
                {
                    this.context.Venta.Remove(venta);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {

                    //return false;

                    throw new Excepciongral($"venta con id: {id} no encontrado.", 404);
                }
            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo eliminar la venta . Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool ActualizarVentaPorId(VentaDTO ventaDTO, int id)
        {
            try
            {
                if (ventaDTO is not null)
                {
                    Usuario? idUsuarioEncontrado = this.context.Usuarios.Where(u => u.Id == ventaDTO.IdUsuario).FirstOrDefault();

                    if (idUsuarioEncontrado is not null)
                    {
                        Ventum? ventaBuscada = this.context.Venta.Where(v => v.Id == id).FirstOrDefault();

                        if (ventaBuscada is not null)
                        {
                            var venta = VentaMapper.MapearVentaDTOAVenta(ventaDTO);
                            this.context.Venta.Update(venta);
                            this.context.SaveChanges();
                            return true;
                        }
                        else
                        {
                            //return false;
                            throw new Excepciongral($"la venta con id : {id} no fue encontrada.", 404);
                        }
                    }
                    else
                    {
                        throw new Excepciongral($"Usuario con id: {ventaDTO.IdUsuario} no encontrado.", 404);
                    }
                }

                //return false;
                throw new Excepciongral("Formato de venta invalido.", 400);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo actualizar la venta . Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool FinalizarVenta(int idUsuario, List<ProductoDTO> listaProductoDTO)
        {
            try
            {

                if (this.usuarioService.ObtenerUsuarioPorID(idUsuario) is not null)
                {

                    var nuevaVenta = new Ventum();

                    var nombreDeProducto = listaProductoDTO.Select(p => p.Descripciones).ToList();

                    nuevaVenta.Comentarios = string.Join("-", nombreDeProducto);
                    nuevaVenta.IdUsuario = idUsuario;



                    this.context.Venta.Add(nuevaVenta);
                    this.context.SaveChanges();


                    this.ActualizarStockProductoYAgregrarProductoVendido(listaProductoDTO, nuevaVenta.Id);

                    return true;
                }

                return false;
                throw new Excepciongral($"Usuario con {idUsuario}, no existe", 404);

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo finalizar la venta . Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public bool ActualizarStockProductoYAgregrarProductoVendido(List<ProductoDTO> listaProductosDTO, int idVenta)
        {

            try
            {
                List<ProductoDTO> nuevaListaProductoDTO = new List<ProductoDTO>();

                foreach (var productoDTO in listaProductosDTO)
                {
                    var productoObtenidoDTODeBD = this.productoService.ObtenerProductoPorID(productoDTO.Id);

                    if (productoObtenidoDTODeBD.Stock <= 0)
                    {
                        continue;
                    }

                    if (productoObtenidoDTODeBD.Stock < productoDTO.Stock)
                    {
                        productoDTO.Stock = productoObtenidoDTODeBD.Stock;

                        nuevaListaProductoDTO.Add(productoDTO);

                        productoObtenidoDTODeBD.Stock = 0;
                        this.productoService.ActualizarStockDeProducto(productoObtenidoDTODeBD);

                        continue;
                    }

                    productoObtenidoDTODeBD.Stock -= productoDTO.Stock;

                    this.productoService.ActualizarStockDeProducto(productoObtenidoDTODeBD);

                    nuevaListaProductoDTO.Add(productoDTO);
                }

                if (nuevaListaProductoDTO is not null)
                {


                    if (MarcarComoProductoVendido(nuevaListaProductoDTO, idVenta))
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se pudo actualizar el stock del producto y agregar producto vendido relacionado con la venta finalizada. Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public bool MarcarComoProductoVendido(List<ProductoDTO> listaProductosDTO, int idVenta)
        {
            try
            {
                var productoVendidoDTO = new ProductoVendidoDTO();


                listaProductosDTO.ForEach(productoDTO =>
                {
                    productoVendidoDTO.IdProducto = productoDTO.Id;
                    productoVendidoDTO.Stock = productoDTO.Stock;
                    productoVendidoDTO.IdVenta = idVenta;
                    this.productoVendidoService.AgregarProductoVendido(productoVendidoDTO);

                });

                return true;
            }
            catch (Excepciongral ex)
            {
                throw new Excepciongral($"No se Marcar como ProductoVendido . Detalle: {ex.Message}", ex.HttpStatusCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
