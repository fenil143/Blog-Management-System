using System;
using System.Collections.Generic;
using System.Linq;
using Blog_Management_System.Data;
using BlogManagement.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public Customer GetById(int id)
    {
        return _context.Customers.Find(id);
    }

    public Customer GetByEmail(string email)
    {
        return _context.Customers.FirstOrDefault(u => u.Email == email);
    }

    public void DeleteById(int id)
    {
        var user = _context.Customers.Find(id);
        if (user != null)
        {
            _context.Customers.Remove(user);
            _context.SaveChanges();
        }
    }

    public List<Customer> GetAll()
    {
        return _context.Customers.ToList();
    }

    public Customer Create(Customer user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Customers.Add(user);
        _context.SaveChanges();
        return user;
    }

    public Customer Update(Customer user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
        return user;
    }

    public void SeedDummyData()
    {
        //if (!_context.Customers.Any())
        if(true)
        {
            List<Customer> dummyUsers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "password1",
                    Phone = "123-456-7890",
                    Address = "123 Main St"
                },
                new Customer
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "password2",
                    Phone = "987-654-3210",
                    Address = "456 Elm St"
                },
                new Customer
                {
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    Password = "password3",
                    Phone = "555-555-5555",
                    Address = "789 Oak St"
                },
                new Customer
                {
                    FirstName = "Bob",
                    LastName = "Wilson",
                    Email = "bob.wilson@example.com",
                    Password = "password4",
                    Phone = "111-222-3333",
                    Address = "321 Birch St"
                },
            };

            _context.Customers.AddRange(dummyUsers);
            _context.SaveChanges();
        }
    }

    public void SetBlog(int id,Blog blog)
    {
        var user = _context.Customers.FirstOrDefault(u => u.Id == id);
        Console.WriteLine(user);
        if (user != null)
        {
            
            blog.User = user;
            //List<Blog> blogs = user.Blogs;
            if (user.Blogs == null)
            {
                user.Blogs = new List<Blog>();
            }
            user.Blogs.Add(blog);
        
            //_context.Customers.Attach(user);
            
            _context.SaveChanges();
        }
    }
    
    public List<Blog> GetBlogs(int id)
    {
        return _context.Customers.FirstOrDefault(u => u.Id == id).Blogs;
    }

}
