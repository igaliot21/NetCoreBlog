using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreBlog.ArchivesManager;
using NetCoreBlog.Controllers.Repository;
using NetCoreBlog.Models;
using NetCoreBlog.ViewModel;
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
        private IFileManager fileManager;
        public PanelController(IRepository Repository, IFileManager FileManager)
        {
            this.repository = Repository;
            this.fileManager = FileManager;
        }
        public IActionResult Index()
        {
            var posts = repository.GetAllPost();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null) return View(new PostViewModel());
            else
            {
                var post = repository.getPost((int)Id);
                //return View(post);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Created = post.Created,
                    Updated = post.Updated
                }) ;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post(vm.Id,vm.Title,vm.Body,await fileManager.SaveImage(vm.Image));

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
                post.Image = await fileManager.SaveImage(vm.Image);

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
