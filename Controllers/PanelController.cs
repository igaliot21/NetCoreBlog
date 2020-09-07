using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreBlog.Controllers.Repository;
using NetCoreBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller   
    {
        private IRepository repository;
        public PanelController(IRepository Repository)
        {
            this.repository = Repository;
        }
        public IActionResult Index()
        {
            var posts = repository.GetAllPost();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null) return View(new Post());
            else
            {
                var post = repository.getPost((int)Id);
                return View(post);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
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
