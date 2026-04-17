using CoffeeTechnik.Models;
using System.Linq;

namespace CoffeeTechnik.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            
            if (!context.Objects.Any())
            {
                context.Objects.AddRange(
                    new ObjectEntity { Name = "Обект 1" },
                    new ObjectEntity { Name = "Обект 2" }
                );

                
                try
                {
                    context.SaveChanges();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Грешка при добавяне на Objects: " + ex.Message);
                }
            }

            
            if (!context.CoffeeMachines.Any())
            {
                
                var firstObject = context.Objects.FirstOrDefault();
                if (firstObject != null)
                {
                    context.CoffeeMachines.AddRange(
                        new CoffeeMachine
                        {
                            Model = "Nespresso",
                            SerialNumber = "NS1001",
                            InstallationDate = null,
                            ObjectEntityId = firstObject.Id
                        },
                        new CoffeeMachine
                        {
                            Model = "DeLonghi",
                            SerialNumber = "DL2001",
                            InstallationDate = null,
                            ObjectEntityId = firstObject.Id
                        },
                        new CoffeeMachine
                        {
                            Model = "Krups",
                            SerialNumber = "KP3001",
                            InstallationDate = null,
                            ObjectEntityId = firstObject.Id
                        }
                    );

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("Грешка при добавяне на CoffeeMachines: " + ex.Message);
                    }
                }
            }
        }
    }
}