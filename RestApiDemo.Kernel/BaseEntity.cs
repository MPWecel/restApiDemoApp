using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.Kernel
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
