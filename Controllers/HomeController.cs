using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using NetCoreBlog.Controllers.Repository;
using NetCoreBlog.Data;
using NetCoreBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        public HomeController(IRepository Repository)
        {
            this.repository = Repository;
        }

        public IActionResult Index() {
            var posts = repository.GetAllPost();
            return View(posts);
        }
        public IActionResult Post(int Id){
            var post = repository.getPost(Id);
            return View(post);
        }
        [HttpGet]
        public IActionResult Edit(int? Id){
            if (Id == null) return View(new Post());
            else {
                var post = repository.getPost((int)Id);
                return View(post);
            } 
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post){
            if (post.Id != 0)
                repository.UpdatePost(post);
            else
                repository.AddPost(post);

            if (await repository.SaveChangesAsync()) return RedirectToAction("Index");
            else return View(post);
        }
        [HttpGet]
        public IActionResult Remove(int Id)
        {
            var post = repository.getPost(Id);
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(Post post)
        {
            repository.RemovePost(post.Id);
            if (await repository.SaveChangesAsync()) return RedirectToAction("Index");
            else return View(post);
        }
    }
}

