using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using PagedList;
using System.IO;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        private BlogDb db = new BlogDb();

        //
        // GET: /Post/

        public ActionResult Index(int? page)
        {
            /**Pagination logic!*/
            var posts = db.Posts.OrderBy(i => i.PostId);

            if (Request.HttpMethod != "GET")
            {
                page = 1;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /Post/Create
    [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Post/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post,Picture picture)
        {
           
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            return View(post);
        }

        //
        // GET: /Post/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //
        // GET: /Post/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(Picture picture, Post p)
        {
            
            foreach (var file in picture.Files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName );
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }
        //
        // POST: /Post/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);

        }

    }
    

    
}