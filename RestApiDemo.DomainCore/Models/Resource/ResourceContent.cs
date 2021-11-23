using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.DomainCore.Models.Resource
{
    public class ResourceContent
    {
        [Key]
        public int Id { get; set; }

        public string Key { get; set; }
        public string Val { get; set; }

        public int ResourceId { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
