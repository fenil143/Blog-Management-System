using System;
using System.Collections.Generic;
using System.Linq;
using BlogManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog_Management_System.Data;

public class BlogRepository : IBlogRepository
{
    private readonly AuthDbContext _context;

    public BlogRepository(AuthDbContext context)
    {
        _context = context;
    }

    public Blog GetById(int id)
    {
        return _context.Blogs.Find(id);
    }

    public void Create(Blog blog)
    {
        if (blog == null)
        {
            throw new ArgumentNullException(nameof(blog));
        }

        _context.Blogs.Add(blog);
        _context.SaveChanges();
    }

    public void Update(Blog blog)
    {
        if (blog == null)
        {
            throw new ArgumentNullException(nameof(blog));
        }
        _context.Blogs.Update(blog);
        //_context.Entry(blog).State = EntityState.Modified;
        _context.SaveChanges();
    }


    public Blog Delete(int id)
    {
        var blog = _context.Blogs.Find(id);
        if (blog != null)
        {
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }

        return blog;
    }

    public bool Count(int id)
    {
        var blog = _context.Blogs.ToList();
        blog = blog.Where(b => b.UserId == id).ToList();
        if (blog.Count > 0) return true;
        return false;
    }

    public List<Blog> GetAll()
    {
        return _context.Blogs.ToList();
    }
}
