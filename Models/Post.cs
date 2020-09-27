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
        public int Id { get; set; }
        [Required]
        [MaxLength(512)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public Post(){}
        public Post(string Title, string Body){
            this.Title = Title;
            this.Body = Body;
        }
        public Post(string Title, string Body, string Image){
            this.Title = Title;
            this.Body = Body;
            this.Image = Image;
        }
        public Post(int Id, string Title, string Body, string Image){
            this.Id = Id;
            this.Title = Title;
            this.Body = Body;
            this.Image = Image;
        }
        public Post(int Id, string Title, string Body, string Image, string Description, string Tags, string Category)
        {
            this.Id = Id;
            this.Title = Title;
            this.Body = Body;
            this.Image = Image;
            this.Description = Description;
            this.Tags = Tags;
            this.Category = Category;
        }
    }
}
