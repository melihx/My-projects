using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadABook.ActionFilters;
using ReadABook.Entities;
using ReadABook.ExtentionMethods;
using ReadABook.Repositories;
using ReadABook.ViewModels.Books;

namespace ReadABook.Controllers
{
    [AuthenticationFilter]
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            BooksDbContext context = new BooksDbContext();

            IndexVM model = new IndexVM();

            //List<int> sharedBookIds = context.BooksToRead
            //                            .Where(i => i.CustomerId == loggedUser.Id)
            //                            .Select(i => i.BookId).ToList();

            model.Books = context.Books
                                    .Where(b => b.ReaderId == loggedUser.Id)
                                    .ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CreateVM model = new CreateVM();
            model.ReaderId = loggedUser.Id;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (model.ReaderId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            BooksDbContext context = new BooksDbContext();

            Book item = new Book();
            item.ReaderId = model.ReaderId;
            item.Title = model.Title;
            item.Author = model.Author;
            item.Summary = model.Summary;

            context.Books.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            BooksDbContext context = new BooksDbContext();
            Book item = context.Books
                                        .Where(p => p.Id == id)
                                        .FirstOrDefault();

            if (item == null)
                return RedirectToAction("Index", "Books");

            if (item.ReaderId != loggedUser.Id)
                return RedirectToAction("Index", "Books");

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.ReaderId = item.ReaderId;
            model.Title = item.Title;
            model.Author = item.Author;
            model.Summary = item.Summary;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            BooksDbContext context = new BooksDbContext();
            Book item = context.Books
                                        .Where(p => p.Id == model.Id)
                                        .FirstOrDefault();

            if (item.ReaderId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            if (model.ReaderId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            item.Id = model.Id;
            item.ReaderId = model.ReaderId;
            item.Title = model.Title;
            item.Author = model.Author;
            item.Summary = model.Summary;

            context.Books.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public IActionResult Share(int Id)
        {
            BooksDbContext context = new BooksDbContext();

            ShareVM model = new ShareVM();
            model.Book = context.Books
                                        .Where(i => i.Id == Id)
                                        .FirstOrDefault();

            model.Shares = context.BooksToRead
                                        .Where(i => i.BookId == Id)
                                        .ToList();

            List<int> usersSharedList = model.Shares
                                                .Select(i => i.ReaderId)
                                                .ToList();
            usersSharedList.Add(model.Book.ReaderId);
            model.Readers = context.Users
                                    .Where(i => !usersSharedList.Contains(i.Id))
                                    .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Share(ShareVM model)
        {
            BooksDbContext context = new BooksDbContext();

            foreach (int userId in model.ReaderIds)
            {
                BookToRead item = new BookToRead();
                item.ReaderId = userId;
                item.BookId = model.BookId;

                context.BooksToRead.Add(item);
                context.SaveChanges();
            }

            return RedirectToAction("Share", "Books", new { Id = model.BookId });
        }

        [HttpGet]
        public IActionResult RevokeShare(int id)
        {
            BooksDbContext context = new BooksDbContext();

            BookToRead item = context.BooksToRead
                                            .Where(i => i.Id == id)
                                            .FirstOrDefault();

            context.BooksToRead.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Share", "Books", new { Id = item.BookId });
        }

        public IActionResult Delete(int id)
        {
            BooksDbContext context = new BooksDbContext();
            Book item = new Book();
            item.Id = id;

            context.Books.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }
    }
}
