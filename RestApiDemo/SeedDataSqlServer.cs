using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApiDemo.DomainCore.Models.Resource;
using RestApiDemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiDemo.Web
{
    public static class SeedDataSqlServer
    {
        public static readonly List<Resource> data = new List<Resource>()
        {
            new Resource()
            {
                Id = 1,
                Name = "Test sample 1",
                ResourceContents = new List<ResourceContent>()
                {
                    new ResourceContent() { Key = "Key1_1"  ,   Val = "Value1_1" },
                    new ResourceContent() { Key = "Key1_2"  ,   Val = "Value1_2" },
                    new ResourceContent() { Key = "Key1_3"  ,   Val = "Value1_3" }
                }
            },
            new Resource()
            {
                Id = 2,
                Name = "Test sample 2",
                ResourceContents = new List<ResourceContent>()
                {
                    new ResourceContent() { Key = "Key2_1"  ,   Val = "Value2_1" },
                    new ResourceContent() { Key = "Key2_2"  ,   Val = "Value2_2" },
                    new ResourceContent() { Key = "Key2_3"  ,   Val = "Value2_3" }
                }
            },
            new Resource()
            {
                Id = 3,
                Name = "AlaMaKota",
                ResourceContents = new List<ResourceContent>()
                {
                    new ResourceContent() { Key = "Ala"  ,   Val = "ma kota" },
                    new ResourceContent() { Key = "Tola"  ,   Val = "ma psa" },
                    new ResourceContent() { Key = "Ola"  ,   Val = "ma rybki" }
                }
            },
            new Resource()
            {
                Id = 4,
                Name = "Liverpool starting 11",
                ResourceContents =
                    (from x in (new Dictionary<string, string>()
                    {
                        { "GK"  ,   "Allison" },
                        { "LB"  ,   "Robertson" },
                        { "LCB" ,   "van Dijk" },
                        { "RCB" ,   "Konate" },
                        { "EB"  ,   "Alexander-Arnold" },
                        { "DM"  ,   "Fabinho" },
                        { "LCM" ,   "Jones" },
                        { "RCM" ,   "Henderson" },
                        { "LS"  ,   "Jota" },
                        { "RS"  ,   "Salah" },
                        { "CF"  ,   "Firmino" }
                    })
                    select new ResourceContent() { Key = x.Key, Val = x.Value}
                    ).ToList()
            }
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                                        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()
                                        )
                  )
            {
                //// Look for any items.
                if (dbContext.Resources.Any())
                    return;   // DB has been seeded

                PopulateTestData(dbContext);
            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            List<Resource> currentDbContent = dbContext.Resources.ToList();

            foreach (var item in currentDbContent)
            {
                dbContext.Resources.Remove(item);
            }
            dbContext.SaveChanges();

            foreach(var seedItem in data)
            {
                Resource newResource = new Resource()
                {
                    Name = seedItem.Name,
                    ResourceContents = seedItem.ResourceContents
                };
                dbContext.Resources.Add(newResource);
            }
            dbContext.SaveChanges();

        }
    }
}
