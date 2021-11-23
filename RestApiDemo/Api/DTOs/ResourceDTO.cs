using System.Collections.Generic;

namespace RestApiDemo.Web.Api.DTOs
{
    public class ResourceDTO : CreateResourceDTO
    {
        public int Id { get; set; }


    }

    public class UpdateResourceDTO : CreateResourceDTO
    {
    }

    public class CreateResourceDTO
    {
        public string Name { get; set; }
        public Dictionary<string, string> ResourceContentDictionary { get; set; }
    }
}
