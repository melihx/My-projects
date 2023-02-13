using Microsoft.AspNetCore.Mvc;
using ReadABook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadABook.Repositories;
using ReadABook.ViewModels.Users;
using ReadABook.ViewModels.Shared;

namespace TermProject.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            BooksDbContext context = new BooksDbContext();

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.PagesCount = (int)Math.Ceiling(context.Users.Count() / (double)model.Pager.ItemsPerPage);

            model.Items = context.Users
                                    .OrderBy(i => i.Id)
                                    .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                                    .Take(model.Pager.ItemsPerPage)
                                    .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User item = new User
            {
                Username = model.Username,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            BooksDbContext context = new BooksDbContext();

            context.Users.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            BooksDbContext context = new BooksDbContext();
            User item = context.Users.Where(u => u.Id == id)
                                     .FirstOrDefault();

            if (item == null)
                return RedirectToAction("Index", "Users");

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            BooksDbContext context = new BooksDbContext();
            context.Users.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }
        public IActionResult Delete(int id)
        {
            BooksDbContext context = new BooksDbContext();
            User item = new User();
            item.Id = id;

            context.Users.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }
    }
}
