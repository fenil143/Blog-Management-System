using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(50)]
        public string BlogType { get; set; }

        [MaxLength(1000000)]
        public Byte[] BlogPhoto { get; set; } 

        public int UserId { get; set; }  // user --> blog :: one --> many
                                         // foreign key 
        public Customer User { get; set; }   // navigation property to access the use


    }
}
