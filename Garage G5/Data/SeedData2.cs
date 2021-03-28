﻿//using Bogus;
//using Garage_G5.Models;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Bogus.Extensions.Sweden;
//using System.Text;

//namespace Garage_G5.Data
//{
//    public static class SeedData2
//    {
//        private static Faker fake;
//        //private static List<TypeOfVehicle> typesOfVehicles;
//        //private static List<Member> members;
//        public static async Task InitAsync(IServiceProvider services)
//        {
//            using (var db = services.GetRequiredService<Garage_G5Context>())
//            {
//                fake = new Faker("sv");
//                if (db.ParkedVehicle.Any() || db.Member.Any() || db.TypeOfVehicle.Any())
//                {
//                    return;
//                }


//                //Add members
//                List<Member> members = GetMembers(20);
//                for (int i = 0; i < 20; i++)
//                {
//                    await db.AddRangeAsync(members);
//                }

//                List<TypeOfVehicle> typeOfVehiclesList = GetTypesOfVehicles();
//                for (int i = 0; i < typeOfVehiclesList.Count(); i++)
//                {
//                    await db.AddRangeAsync(typeOfVehiclesList[i]);
//                }

//                List<ParkedVehicle> vehiclesList = GetVehicles(21);
//                var vehicles = new List<ParkedVehicle>();
//                foreach (var member in members)
//                {
//                    foreach (var item in collection)
//                    {

//                    }

//                }
//                for (int i = 0; i < vehiclesList.Count(); i++)
//                {
//                    //try
//                    //{
//                    await db.AddRangeAsync(vehiclesList[i]);
//                    //await db.SaveChangesAsync();
//                    //}
//                    //catch (Exception e)
//                    //{

//                    //}
//                }

//                //Update the DB
//                await db.SaveChangesAsync();
//            }


//        }

//        private static List<Member> GetMembers(int amount)
//        {
//            var members  = new List<Member>();
//            DateTime startDateTime = new DateTime(2020, 01, 01);
//            for (int i = 0; i < amount; i++)
//            {
//                int first4digits = fake.Random.Int(1939, 2010);
//                var personalIdNumber = new StringBuilder();
//                personalIdNumber.Append(first4digits);
//                personalIdNumber.Append(fake.Random.Int(11, 12));
//                personalIdNumber.Append(fake.Random.Int(10, 28));
//                personalIdNumber.Append(fake.Random.Int(0001, 9999));

//                var member = new Member
//                {
//                    FirstName = fake.Name.FirstName(),
//                    LastName = fake.Name.LastName(),
//                    MembershipType = (MembershipType)0,
//                    Phone = fake.Phone.PhoneNumberFormat(),
//                    PersonalIdNumber = personalIdNumber.ToString(), //198010101234

//                    Age = DateTime.Now.Year - first4digits,
//                    DateAdded = fake.Date.Between(startDateTime, DateTime.Now),
//                    BonusAccountExpires = fake.Date.Between(startDateTime, DateTime.Now)
//                };

//                members.Add(member);
//            }

//            return members;
//        }

//        private static List<TypeOfVehicle> GetTypesOfVehicles()
//        {
//          var   typesOfVehicles = new List<TypeOfVehicle>() {
//                new TypeOfVehicle {
//                    TypeName = "Motorcycle",
//                    Size = 1,
//                },
//                new TypeOfVehicle{
//                    TypeName = "Combi",
//                    Size = 3,
//                },
//                new TypeOfVehicle{
//                    TypeName = "Sedan",
//                    Size = 3,
//                },
//                new TypeOfVehicle {
//                    TypeName = "Coupe",
//                    Size = 3,
//                },
//                new TypeOfVehicle{
//                    TypeName = "Van",
//                    Size = 3,
//                },
//                new TypeOfVehicle {
//                    TypeName = "Roadster",
//                    Size = 3,
//                },
//                new TypeOfVehicle{
//                    TypeName = "MiniVan",
//                    Size = 3,
//                },
//                new TypeOfVehicle{
//                    TypeName = "Truck",
//                    Size = 6,
//                },
//                new TypeOfVehicle{
//                    TypeName = "BigTruck",
//                    Size = 6,
//                },
//                new TypeOfVehicle{
//                    TypeName = "Boat",
//                    Size = 9,
//                },
//                new TypeOfVehicle{
//                    TypeName = "Airplane",
//                    Size = 9,
//                },
//            };
//            return typesOfVehicles;
//        }

//        private static List<ParkedVehicle> GetVehicles(int amount)
//        {
//            var vehicles = new List<ParkedVehicle>();

//            DateTime startDateTime = new DateTime(2020, 01, 01);

//            for (int i = 0; i < amount; i++)
//            {

//                var vehicle = new ParkedVehicle
//                {
//                    //VehicleType = fake.PickRandom<VehicleType>(),
//                    RegistrationNum = fake.Random.AlphaNumeric(6),
//                    Color = fake.Commerce.Color(),
//                    Brand = fake.Vehicle.Manufacturer(),
//                    Model = fake.Vehicle.Model(),
//                    WheelsNum = 4,
//                    EnteringTime = fake.Date.Between(startDateTime, DateTime.Now),
//                    //TypeOfVehicle = fake.Random.ListItem<TypeOfVehicle>(typesOfVehicles),
//                    //Member = fake.Random.ListItem<Member>(members),
//                    IsInGarage = fake.Random.Bool()
//                };

//                vehicles.Add(vehicle);
//            }

//            return vehicles;
//        }
//    }
//}