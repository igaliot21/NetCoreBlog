using NetCoreBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.Controllers.Repository
{
    public interface IRepository
    {
        Post getPost(int Id);
        List<Post> GetAllPost();
        List<Post> GetAllPost(string Category);
        List<Post> GetPostDates(DateTime Initial, DateTime End);
        bool RemovePost(int Id);
        bool UpdatePost(Post post);
        void AddPost(Post post);
        Task<bool> SaveChangesAsync();
    }
}
