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
    }
}

