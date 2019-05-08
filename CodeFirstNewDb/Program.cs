using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstNewDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new GeoContext())
            {
                Console.Write("Enter a name to enter a new country");
                var name = Console.ReadLine();

                var country = new Country { Name = name };
                db.Countries.Add(country);
                db.SaveChanges();

                var query = from b in db.Countries
                            orderby b.Name
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual List<Country> Countries { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }

    public class GeoContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
