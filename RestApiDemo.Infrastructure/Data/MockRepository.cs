using RestApiDemo.DomainCore.Models.Resource;
using RestApiDemo.Kernel.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiDemo.Infrastructure.Data
{
    class MockRepository : IRepository<Resource>
    {
        //private DbSet<Resource> _inMemoryResourceMockedDbSet;
        private volatile List<Resource> _inMemoryResourceMockedDbSet;

        public MockRepository()
        {
            //_inMemoryResourceMockedDbSet = GenerateResourceList().AsQueryable().BuildMockDbSet().Object;
            _inMemoryResourceMockedDbSet = GenerateResourceList();
        }

        public async Task<Resource> AddAsync(Resource entity)
        {
            //return (await _inMemoryResourceMockedDbSet.AddAsync(entity).AsTask()).Entity;
            int maxId = _inMemoryResourceMockedDbSet.Select(x => x.Id).Max();
            entity.Id = maxId + 1;
            _inMemoryResourceMockedDbSet.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(Resource entity)
        {
            //Resource toDelete = await _inMemoryResourceMockedDbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            Resource toDelete = _inMemoryResourceMockedDbSet.Where(x => x.Id == entity.Id).FirstOrDefault();

            if (toDelete != null)
            {
                _inMemoryResourceMockedDbSet.Remove(toDelete);
            }
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            //return await _inMemoryResourceMockedDbSet.ToListAsync();
            return _inMemoryResourceMockedDbSet.ToList();
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            //return await _inMemoryResourceMockedDbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _inMemoryResourceMockedDbSet.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task UpdateAsync(Resource entity)
        {
            //Resource toUpdate = await _inMemoryResourceMockedDbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            Resource toUpdate = _inMemoryResourceMockedDbSet.Where(x => x.Id == entity.Id).FirstOrDefault();

            if (toUpdate != null)
            {
                //_inMemoryResourceMockedDbSet.Update(entity);
                toUpdate.Name = entity.Name;
                //toUpdate.ResourceContentDictionary = entity.ResourceContentDictionary;
                toUpdate.ResourceContents = entity.ResourceContents;
            }

        }

        private static List<Resource> GenerateResourceList()
        {
            List<Resource> result = new List<Resource>();

            result.Add(
                new Resource()
                {
                    Id=1,
                    Name = "TestResource1",
                    //ResourceContentDictionary = new Dictionary<string, string>()
                    //{
                    //    { "Key1_1"  ,   "Value1_1" },
                    //    { "Key1_2"  ,   "Value1_2" },
                    //    { "Key1_3"  ,   "Value1_3" }
                    //}
                    ResourceContents = new List<ResourceContent>()
                    {
                        new ResourceContent() { Key = "Key1_1"  ,   Val = "Value1_1" },
                        new ResourceContent() { Key = "Key1_2"  ,   Val = "Value1_2" },
                        new ResourceContent() { Key = "Key1_3"  ,   Val = "Value1_3" },
                    }
                }
            );

            result.Add(
                new Resource()
                {
                    Id = 2,
                    Name = "TestResource2",
                    //ResourceContentDictionary = new Dictionary<string, string>()
                    //{
                    //    { "Key2_1"  ,   "Value2_1" },
                    //    { "Key2_2"  ,   "Value2_2" },
                    //    { "Key2_3"  ,   "Value2_3" }
                    //}
                    ResourceContents = new List<ResourceContent>()
                    {
                        new ResourceContent() { Key = "Key2_1"  ,   Val = "Value2_1" },
                        new ResourceContent() { Key = "Key2_2"  ,   Val = "Value2_2" },
                        new ResourceContent() { Key = "Key2_3"  ,   Val = "Value2_3" },
                    }
                }
            );

            result.Add(
                new Resource()
                {
                    Id = 3,
                    Name = "Alamakota",
                    //ResourceContentDictionary = new Dictionary<string, string>()
                    //{
                    //    { "Ala"     ,   "ma kota" },
                    //    { "Tola"    ,   "ma psa" },
                    //    { "Ola"     ,   "ma rybki" }
                    //}
                    ResourceContents = new List<ResourceContent>()
                    {
                        new ResourceContent() { Key = "Ala"  ,   Val = "ma kota" },
                        new ResourceContent() { Key = "Tola"  ,   Val = "ma psa" },
                        new ResourceContent() { Key = "Ola"  ,   Val = "ma rybki" },
                    }
                }
            );

            result.Add(
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
            );

            return result;
        }
    }
}
