using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(512)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
