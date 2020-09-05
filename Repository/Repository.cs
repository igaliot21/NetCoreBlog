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
            if (postToReturn == null) return new Post("Post Not Found", "Post Not Found");
            else return postToReturn;
        }
        
        public List<Post> GetAllPost(){
            return this.context.Posts.ToList();
        }
        
        public List<Post> GetPostDates(DateTime Initial, DateTime End){
            return this.context.Posts.Where(p => p.Created >= Initial && p.Created <= End).ToList();
        }

        public void AddPost(Post post){
            this.context.Posts.Add(post);
        }

        public bool UpdatePost(int Id, Post post){
            var postToUpdate = this.context.Posts.FirstOrDefault(p => p.Id == Id);
            
            if (postToUpdate == null) return false;
            
            if (postToUpdate.Id != post.Id) return false;
            
            postToUpdate.Title = post.Title;
            postToUpdate.Body = post.Body;
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
