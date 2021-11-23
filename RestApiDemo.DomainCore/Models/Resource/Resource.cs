using RestApiDemo.Kernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.DomainCore.Models.Resource
{
    public class Resource : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        //public Dictionary<string, string> ResourceContentDictionary { get; set; }
        [Required]
        public List<ResourceContent> ResourceContents { get; set; }

    }
}
