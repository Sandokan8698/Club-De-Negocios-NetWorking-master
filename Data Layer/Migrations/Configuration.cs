using Data_Layer.Implementations;
using Domain_Layer.Entities;
using Faker.Generator;
using Microsoft.Win32;

namespace Data_Layer.Migrations
{
    using Faker;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data_Layer.ClubNegociosNetworkingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data_Layer.ClubNegociosNetworkingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.ProveedorDbSet.AddOrUpdate(
                    new Proveedor {ProveedorId = 1, Nombre = "Nozztra" },
                    new Proveedor { ProveedorId = 2, Nombre = "Seductora" },
                    new Proveedor { ProveedorId = 3, Nombre = "JSN" },
                    new Proveedor {ProveedorId = 4,Nombre = "Mistika" },
                    new Proveedor { ProveedorId = 5, Nombre = "Hechizada" }
                );
        }
    }
}
