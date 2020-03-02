using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CS.NET_Capstone_4___TaskListRevisited.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS.NET_Capstone_4___TaskListRevisited.Controllers
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private readonly CPSTN4_TaskListRevContext _context;

        public UserTasksController (CPSTN4_TaskListRevContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<UserTasks> thisUserTasks = _context.UserTasks.Where(x => x.OwnerId == id).ToList();
            string currentUser = _context.AspNetUsers.Find(id).Email;
            ViewBag.Current = currentUser;
            return View(thisUserTasks);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(UserTasks newTask)
        {
            newTask.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if(ModelState.IsValid)
            {
                _context.UserTasks.Add(newTask);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            UserTasks foundTask = _context.UserTasks.Find(id);
            if(foundTask != null)
            {
                return View(foundTask);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditTask(UserTasks updatedTask)
        {
            UserTasks dbUserTasks = _context.UserTasks.Find(updatedTask.Id);
            if (ModelState.IsValid)
            {
                dbUserTasks.Description = updatedTask.Description;
                dbUserTasks.DueDate = updatedTask.DueDate;
                dbUserTasks.Complete = updatedTask.Complete;

                _context.Entry(dbUserTasks).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(dbUserTasks);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteEmployee(int id)
        {
            UserTasks foundTask = _context.UserTasks.Find(id);
            if(foundTask != null)
            {
                _context.Remove(foundTask);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}