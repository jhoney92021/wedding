using System;//SO YOU DONT HAVE TO SAY System.Console
using System.Collections.Generic;//TO USE LISTS
using System.Diagnostics;
using System.Linq;//GIVES ACCESS TO LANGUAGE INTEGRATED QUERY
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;//TO USE HASHER
using Microsoft.AspNetCore.Mvc;//TO USE MVC FRAMEWORK
using Microsoft.AspNetCore.Http;//TO USE SESSION
using Microsoft.EntityFrameworkCore;//TO BE ABLE TO USE .Include()
using Newtonsoft.Json;//TO USE JSON
using WeddingPlanner.Models;//TO USE MODELS PAGE

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public WeddingController(MyContext context)
        {
            dbContext = context;
        }
        //localhost:5000/Wedding/Index
        //localhost:5000/Wedding/Index
        [Route("Wedding")]
        [HttpGet]
        public IActionResult Index()
        {
            User planner = dbContext.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("liveUser"));
            Console.WriteLine($"-----------{planner.UserId} Was Here-----------");
            ViewBag.plannerId = planner.UserId;
            Console.WriteLine($"-----------{ViewBag.plannerId} Was Here-----------");
            return View();
        }
        //localhost:5000/Wedding/Index
        //localhost:5000/Wedding/Index
        //localhost:5000/Wedding/Create
        //localhost:5000/Wedding/Create
        [HttpPost("Wedding/Create")]
        public IActionResult Create(Wedding newWedding)
        {            
            if(ModelState.IsValid)
            {
                Console.WriteLine($"-----------{newWedding.Groom} Was Here-----------");
                var checker =  newWedding.Date - DateTime.Today.Date;
                int result = DateTime.Compare(newWedding.Date, DateTime.Today.Date);
                Console.WriteLine($"-----------{result} Was Here-----------");
                //CHECK THAT DATE IS IN FUTURE
                if(result <= 0)
                {
                    ModelState.AddModelError("Date", "Date must be in the Future");
                    return View("Index", "Wedding");
                }
                //CHECK THAT DATE IS IN FUTURE
                User planner = dbContext.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("liveUser"));
                
                //MAKE NEW USER
                dbContext.Weddings.Add(newWedding);
                dbContext.SaveChanges();
                //MAKE NEW USER
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                Console.WriteLine("-----------Wedding Creation Route-----------");
                Console.WriteLine("-----------//localhost:5000/Wedding/Create-----------");
                Console.WriteLine($"-----------{newWedding.Bride}-----------");
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                return RedirectToAction("View", new{id=newWedding.WeddingId});
            }
            else
            {
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                Console.WriteLine("-----------Wedding Creation Validation Fail Route-----------");
                return View("Index");
            }
        }
        //localhost:5000/Wedding/Create
        //localhost:5000/Wedding/Create
        //localhost:5000/Wedding/View
        //localhost:5000/Wedding/View
        [HttpGet("Wedding/View/{id}")]
        public IActionResult View(int id)
        {
            Wedding thisWedding = dbContext.Weddings
            .Include(w => w.Guests)
            .ThenInclude(g => g.Attendee)
            .FirstOrDefault(w => w.WeddingId == id);
            return View(thisWedding);
        }
        //localhost:5000/Wedding/View
        //localhost:5000/Wedding/View
        //localhost:5000/Wedding/Dashboard
        //localhost:5000/Wedding/Dashboard
        [HttpGet("Wedding/Dashboard")]
        public IActionResult Dashboard()
        {
            User liveUser = dbContext.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("liveUser"));
            ViewBag.liveUser = liveUser;
            List<Wedding> allWeddings = dbContext.Weddings
            .OrderByDescending(w => w.CreatedAt)
            .Include(w => w.Planner)
            .Include(w => w.Guests)
            .ToList();
            return View(allWeddings);
        }
        //localhost:5000/Wedding/Dashboard
        //localhost:5000/Wedding/Dashboard
        //localhost:5000/Wedding/Join
        //localhost:5000/Wedding/Join
        [HttpGet("Wedding/Join/{id}")]
        public IActionResult Join(int id)
        {
            Console.WriteLine("-----------Join Wedding Route-----------");
            User liveUser = dbContext.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("liveUser"));
            Wedding thisWedding = dbContext.Weddings.FirstOrDefault(Wedding => Wedding.WeddingId == id);
            Guest newGuest = new Guest( );
            newGuest.UserId = liveUser.UserId;
            newGuest.WeddingId = thisWedding.WeddingId;
            dbContext.Add(newGuest);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
        //localhost:5000/Wedding/Join
        //localhost:5000/Wedding/Join
        //localhost:5000/Wedding/Leave
        //localhost:5000/Wedding/Leave
        [HttpGet("Wedding/Leave/{id}")]
        public IActionResult Leave(int id)
        {            
            Guest thisGuest = dbContext.Guests.FirstOrDefault(g => g.WeddingId == id && g.UserId == HttpContext.Session.GetInt32("liveUser"));
            dbContext.Guests.Remove(thisGuest);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        //localhost:5000/Wedding/Leave
        //localhost:5000/Wedding/Leave
        //localhost:5000/Wedding/Delete
        //localhost:5000/Wedding/Delete
        [HttpGet("Wedding/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Wedding toDelete = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == id);
            dbContext.Weddings.Remove(toDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        //localhost:5000/Wedding/Delete
        //localhost:5000/Wedding/Delete
    }
}
