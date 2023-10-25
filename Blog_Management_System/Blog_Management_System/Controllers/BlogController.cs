using BlogManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog_Management_System.Controllers
{
    public class BlogController : Controller
    {
        public readonly IBlogRepository _blogRepository;
        public readonly IUserRepository _userRepository;

        public BlogController(IBlogRepository br, IUserRepository userRepository)
        {
            _blogRepository = br;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            if(GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["class1"] = "active";
            return View();
        }


        [HttpPost]
        public IActionResult Create(Blog model, IFormFile BlogPhoto)
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            Blog blog = model;
            if (BlogPhoto != null && BlogPhoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                     BlogPhoto.CopyTo(memoryStream);
                    byte[] photoData = memoryStream.ToArray();


                    blog = new Blog
                    {
                        Title = model.Title,
                        Content = model.Content,
                        IsPublic = model.IsPublic,
                        CreatedAt = DateTime.Now,
                        BlogType = model.BlogType,
                        BlogPhoto = photoData, 
                        //UserId = GetCurrentUserId(),
                    };

                    
                    //_blogRepository.Create(blog);

                    //return RedirectToAction("PreviousBlogs");
                }
            }

            if (blog != null)
            {
                _userRepository.SetBlog(GetCurrentUserId(), blog);

                return RedirectToAction("PreviousBlogs");

            }
            return View(blog);
        }

        public IActionResult PreviousBlogs()
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            //List<Blog> blogs = _userRepository.GetBlogs(GetCurrentUserId());
            List<Blog> blogs = _blogRepository.GetAll();
            List<Blog> blogsForUser = blogs.Where(b => b.UserId == GetCurrentUserId()).ToList();
            if(blogsForUser == null)
            {
                return RedirectToAction("Create");
            }
            ViewData["class2"] = "active";
            return View(blogsForUser);
        }

        public IActionResult Delete(int id)
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            _blogRepository.Delete(id);
            return RedirectToAction("PreviousBlogs");
        }
        public IActionResult Edit(int id)
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            Blog blog = _blogRepository.GetById(id);
            ViewData["class2"] = "active";
            return View(blog);
        }

        [HttpPost]
        public IActionResult Update(Blog updatedBlog, IFormFile BlogPhoto)
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            Blog blog = _blogRepository.GetById(updatedBlog.Id);
            if (BlogPhoto != null && BlogPhoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    BlogPhoto.CopyTo(memoryStream);
                    byte[] photoData = memoryStream.ToArray();
                    blog.BlogPhoto = photoData;
                    
                }
            }
            blog.Title = updatedBlog.Title;
            blog.Content = updatedBlog.Content;
            blog.BlogType = updatedBlog.BlogType;
            blog.IsPublic = updatedBlog.IsPublic;
            _blogRepository.Update(blog);

            return RedirectToAction("PreviousBlogs");
        }

        public IActionResult UserBlogs(int userId)
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Blog> blogs = _blogRepository.GetAll();
            List<Blog> blogsForUser = blogs.Where(b => b.UserId == userId).ToList();
            ViewData["class3"] = "active";
            return View(blogsForUser);
        }

        public IActionResult UserList()
        {
            if (GetCurrentUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Customer> users = _userRepository.GetAll();
            users = users.Where(b => _blogRepository.Count(b.Id)).ToList();
            ViewData["class3"] = "active";
            return View(users);
        }

        public int GetCurrentUserId()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return 0;
            }
            return (int)HttpContext.Session.GetInt32("UserId"); 
        }
    }
}
