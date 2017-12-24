using System;
using Data_Layer;
using Data_Layer.Implementations;
using Data_Layer.Implementations.Repositories;
using Domain_Layer.Entities;
using Faker;
using Faker.Generator;
namespace TestWhatIDontKno
{
    class Program
    {
        static void Main(string[] args)
        {

            //var ventaFactory = new Faker<Venta>();
            //var ventas = ventaFactory.CreateMany(1000, v =>
            //{
            //    v.ClienteId = new IntegerGenerator().Get(1, 500);
            //    v.Cliente = null;
            //    v.TrabajadorId = 1;
            //    v.Trabajador = null;

            //    v.Fecha = GetDate();

            //});

            //using (var uw = new UnitOfWork(new ClubNegociosNetworkingContext()))
            //{
            //    uw.VentaRepository.AddRange(ventas);
            //    uw.Complete();
            //}

            //var pedidoFactory = new Faker<Pedido>();
            //var pedidos = pedidoFactory.CreateMany(10000, p =>
            //{
            //    p.ProveedorId = new IntegerGenerator().Get(1, 6);
            //    p.Proveedor = null;
            //    p.ItemNumero = new IntegerGenerator().Get(1, 200);
            //    p.VentaId = new IntegerGenerator().Get(1, 1000);
            //    p.Venta = null;

            //});

            //using (var uw = new UnitOfWork(new ClubNegociosNetworkingContext()))
            //{
            //    uw.PedidoRepositoy.AddRange(pedidos);
            //    uw.Complete();
            //}

            var subPedidoFactory = new Faker<SubPedido>();
            var subPedidos = subPedidoFactory.CreateMany(10000, sb =>
            {
                sb.PedidoId = new IntegerGenerator().Get(1, 1000);
                sb.FechaCreacion = GetDate();
                sb.Identificador = new IntegerGenerator().Get(150000, 990000000).ToString();
                sb.PrecioProveedor = new IntegerGenerator().Get(20, 150);
                sb.Pedido = null;
                sb.SubPedidosEntrys.Add(new Faker<SubPedidoEntry>().Create(sbe =>
                {
                    sbe.Abono = new IntegerGenerator().Get(int.Parse(sb.PrecioProveedor.ToString()), 150);
                    sbe.FechaCreacion = sb.FechaCreacion;
                    sbe.SubPedido = null;
                    sbe.TrabajadorId = 1;
                    sbe.Trabajador = null;
                }));

            });

            using (var uw = new UnitOfWork(new ClubNegociosNetworkingContext()))
            {
                // subPedidos.ToList().ForEach(sb => uw.SubPedidoRepository.Add(sb));
                uw.SubPedidoRepository.AddRange(subPedidos);

                uw.Complete();
            }

            //var otro = new Faker<Cliente>();
            //var clientes = otro.CreateMany(500, v =>
            //{
            //    v.Nombre = new NameGenerator().Get();  // Name generator will generate real names like Jhon Doe 1, Bruno Matarazo 2.
            //    v.NumeroDocumento = new IntegerGenerator().Get(100000000, 1000000000).ToString();
            //    v.Apellidos = new NameGenerator().Get(10);
            //    v.Telefono = new IntegerGenerator().Get(100000000, 1000000000).ToString();
            //    v.FechaNacimiento = new DateTimeGenerator().Get();
            //});

            //using (var uw = new UnitOfWork(new ClubNegociosNetworkingContext()))
            //{
            //    // subPedidos.ToList().ForEach(sb => uw.SubPedidoRepository.Add(sb));
            //    uw.ClienteRepository.AddRange(clientes);

            //    uw.Complete();
            //}
        }

        static DateTime GetDate()
        {
            Random r = new Random();
            DateTime rDate = new DateTime(r.Next(2016, 2017), r.Next(1, 12), r.Next(1, 28)).Date;
            return rDate;
        }
    }


    
}
