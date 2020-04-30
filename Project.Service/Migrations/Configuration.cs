namespace Project.Service.Migrations
{
    using Project.Service.Domain;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Project.Service.VehicleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Project.Service.VehicleContext";
        }

        protected override void Seed(Project.Service.VehicleContext context)
        {
           

            var vehiclemakes = new List<VehicleMake>
            {
            new VehicleMake{Name="Bayerische Motoren Werke",Abbreviation="BMW"},
            new VehicleMake{Name="Ford Motor Company",Abbreviation="Ford"},
            new VehicleMake{Name="Volkswagen",Abbreviation="VW"},

            };

            vehiclemakes.ForEach(v => context.VehicleMakes.Add(v));
            context.SaveChanges();
            var vehiclemodels = new List<VehicleModel>
            {
            new VehicleModel{Name="X5",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModel{Name="X6",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModel{Name="X1",Abbreviation= "BMW",VehicleMakeId = 1},
            new VehicleModel{Name="Mondeo",Abbreviation= "Ford",VehicleMakeId = 2},
            new VehicleModel{Name="Fiesta",Abbreviation= "Ford",VehicleMakeId = 2},
            new VehicleModel{Name="Golf5",Abbreviation= "VW",VehicleMakeId = 3},
            new VehicleModel{Name="Golf4",Abbreviation= "VW",VehicleMakeId = 3},

            };
            vehiclemodels.ForEach(v => context.VehicleModels.Add(v));
            context.SaveChanges();


        }
    }
}
