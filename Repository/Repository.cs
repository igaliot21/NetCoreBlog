using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NetCoreBlog.Data;
using NetCoreBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.Controllers.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext context;
        
        public Repository(AppDbContext Context){
            this.context = Context;
        }
        
        public Post getPost(int Id){
            var postToReturn = this.context.Posts.FirstOrDefault(p => p.Id == Id);
            if (postToReturn == null) postToReturn = new Post("Post Not Found", "Post Not Found");
            
            return postToReturn;
        }
        
        public List<Post> GetAllPost(){
            var posts = this.context.Posts.ToList();
            if (posts.Count == 0) posts.Add(new Post("No Posts found", "No Posts found"));
            
            return posts;
        }
        
        public List<Post> GetPostDates(DateTime Initial, DateTime End){
            var posts = this.context.Posts.Where(p => p.Created >= Initial && p.Created <= End).ToList();
            if (posts.Count == 0) posts.Add(new Post("No Posts found", "No Posts found"));
            
            return posts;
        }

        public void AddPost(Post post){
            this.context.Posts.Add(post);
        }

        public bool UpdatePost(Post post){
            var postToUpdate = this.context.Posts.FirstOrDefault(p => p.Id == post.Id);
            
            if (postToUpdate == null) return false;
            
            postToUpdate.Title = post.Title;
            postToUpdate.Body = post.Body;
            postToUpdate.Image = post.Image;
            postToUpdate.Updated = DateTime.Now;
            
            this.context.Posts.Update(postToUpdate);
            return true;
        }

        public bool RemovePost(int Id){
            var postToDelete = this.context.Posts.Single(p => p.Id == Id);
            if (postToDelete == null) return false;

            this.context.Posts.Remove(postToDelete);
            return true;
        }

        public async Task<bool> SaveChangesAsync() {
            if (await this.context.SaveChangesAsync() > 0) return true;
            else return false;
        }

    }
}
