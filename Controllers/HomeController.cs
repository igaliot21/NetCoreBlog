using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using NetCoreBlog.ArchivesManager;
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
        private IFileManager fileManager;
        public HomeController(IRepository Repository, IFileManager FileManager)
        {
            this.repository = Repository;
            this.fileManager = FileManager;
        }

        public IActionResult Index() {
            var posts = repository.GetAllPost();
            return View(posts);
        }
        public IActionResult Post(int Id){
            var post = repository.getPost(Id);
            return View(post);
        }
        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image) {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}