using Microsoft.AspNetCore.Mvc;
using RestApiDemo.DomainCore.Models.Resource;
using RestApiDemo.Kernel.Interfaces;
using RestApiDemo.Web.Api.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiDemo.Web.Api.Controllers
{
    public class ResourcesApiController : BaseApiController
    {
        private readonly IRepository<Resource> _repository;

        public ResourcesApiController(IRepository<Resource> repo)
        {
            _repository = repo;
        }

        // GET: api/Resources
        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            List<Resource> resourceList = (await _repository.GetAllAsync());

            //foreach(var x in resourceList)
            //{
            //    string output = String.Format("Resource:\t\tId:\t{0}\t\tName:\t{1}\nContents:", x.Id, x.Name);
            //    System.Diagnostics.Debug.WriteLine(output);
            //    foreach(var kvp in x.ResourceContents)
            //    {
            //        string output2 = String.Format("\t\t\tKey:\t{0}\t\tValue:\t{1}", kvp.Key, kvp.Val);
            //    }
            //}

            List<ResourceDTO> resourceDtoList =
                (resourceList)
                .Select(
                    res => new ResourceDTO
                    {
                        Id = res.Id,
                        Name = res.Name,
                        ResourceContentDictionary = res.ResourceContents.ToDictionary(x=> x.Key, x=>x.Val)
                    }
                ).ToList();

            return Ok(resourceDtoList);
        }

        // GET: api/Resources/{Id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetResourceById(int id)
        {
            Resource resource = (await _repository.GetByIdAsync(id));

            if(resource!=null)
            {
                ResourceDTO result = new ResourceDTO()
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    ResourceContentDictionary = resource.ResourceContents.ToDictionary(x => x.Key, x => x.Val)
                };

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Resources
        [HttpPost]
        public async Task<IActionResult> CreateResource([FromBody] CreateResourceDTO request)
        {
            Resource newResource = new Resource()
            {
                Name = request.Name,
                ResourceContents =  (from kvp in request.ResourceContentDictionary
                                     select new ResourceContent()
                                     {
                                         Key = kvp.Key,
                                         Val = kvp.Value
                                     }
                                    ).ToList()
            };

            Resource createdResource = (await _repository.AddAsync(newResource));

            ResourceDTO result = new ResourceDTO()
            {
                Id = createdResource.Id,
                Name = createdResource.Name,
                ResourceContentDictionary = createdResource.ResourceContents.ToDictionary(x => x.Key, x => x.Val)
            };

            //return CreatedAtAction(nameof(GetResourceById), new { id = createdResource.Id }, createdResource); //Created Url of a resource - needed
            return Ok(result);
        }

        // PUT: api/Resources/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, [FromBody] UpdateResourceDTO request)
        {
            Resource resourceFromRepo = (await _repository.GetByIdAsync(id));

            if(resourceFromRepo==null)
            {
                return NotFound();
            }
            else
            {
                resourceFromRepo.Name = request.Name;
                resourceFromRepo.ResourceContents = (from kvp in request.ResourceContentDictionary
                                                     select new ResourceContent()
                                                     {
                                                         Key = kvp.Key,
                                                         Val = kvp.Value
                                                     }
                                                    ).ToList();

                await _repository.UpdateAsync(resourceFromRepo);

                return NoContent();
                //return Ok(resourceFromRepo);
            }
        }

        //DELETE api/Resources/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            Resource resourceFromRepo = (await _repository.GetByIdAsync(id));

            if (resourceFromRepo == null)
            {
                return NotFound();
            }
            else
            {
                await _repository.DeleteAsync(resourceFromRepo);

                return NoContent();
            }
        }
    }
}
