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

            return View();
        }
        public IActionResult Post(){

            return View();
        }
        [HttpGet]
        public IActionResult Edit(){

            return View(new Post());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post){
            this.repository.AddPost(post);
            if (await this.repository.SaveChangesAsync()) return RedirectToAction("Index");
            else return View(post);
        }
    }
}

