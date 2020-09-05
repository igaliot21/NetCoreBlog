using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
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
        private AppDbContext context;
        public HomeController(AppDbContext Context)
        {
            this.context = Context;
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

            this.context.Posts.Add(post);
            await this.context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

