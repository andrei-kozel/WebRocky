using System.ComponentModel.DataAnnotations;

namespace WebRocky.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
