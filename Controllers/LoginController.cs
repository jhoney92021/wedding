using System;//SO YOU DONT HAVE TO SAY System.Console
using System.Collections.Generic;//TO USE LISTS
using System.Diagnostics;
using System.Linq;//GIVES ACCESS TO LANGUAGE INTEGRATED QUERY
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;//TO USE HASHER
using Microsoft.AspNetCore.Mvc;//TO USE MVC FRAMEWORK
using Microsoft.AspNetCore.Http;//TO USE SESSION
using Newtonsoft.Json;//TO USE JSON
using WeddingPlanner.Models;//TO USE MODELS PAGE

namespace WeddingPlanner.Controllers
{
    public class LoginController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public LoginController(MyContext context)
        {
            dbContext = context;
        }
        //localhost:5000/
        //localhost:5000/
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //localhost:5000/
        //localhost:5000/
        //localhost:5000/Login/LoginTemplate
        //localhost:5000/Login/LoginTemplate
        [HttpGet("Login/LoginTemplate")]
        public IActionResult LoginTemplate()
        {
            //STATEMENTS TO DEBUG IF ROUTE IS HIT
            Console.WriteLine("-----------Login Validation Fail Route-----------");
            return View();
        }
        //localhost:5000/Login/LoginTemplate
        //localhost:5000/Login/LoginTemplate
        //localhost:5000/Login/Login
        //localhost:5000/Login/Login
        [HttpPost("Login/Login")]
        public IActionResult Login(LoginUser thisUser)
        {
            if(ModelState.IsValid)
            {
                //GRAB THE USERS INFO
                User liveUser = dbContext.Users.FirstOrDefault(user => user.Email == thisUser.Email);
                //CHECK IF QUERY RETURNED A USER
                if(liveUser == null){ModelState.AddModelError("Email", "Login Failed");return View("Index");}

                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(thisUser, liveUser.Password, thisUser.Password);
                //CHECK IF QUERY MATCHED A PASSWORD TO AN EMAIL
                if(result == 0){ModelState.AddModelError("Password", "Login Failed");return View("Index");}
                Console.WriteLine($"-----------{liveUser.Fname}-----------");
                //PUT NEW USERS ID INTO SESSION
                HttpContext.Session.SetInt32("liveUser", liveUser.UserId);
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                Console.WriteLine("-----------User Login Route-----------");
                Console.WriteLine("-----------//localhost:5000/User/Login-----------");
                return RedirectToAction("Dashboard", "Wedding");
            }
            else
            {
                User liveUser = dbContext.Users.FirstOrDefault(user => user.Email == thisUser.Email);
                Console.WriteLine($"-----------{liveUser.Fname}-----------");
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                Console.WriteLine("-----------User Login Validation Fail Route-----------");
                return View("Index");
            }
        }
        //localhost:5000/Login/Login
        //localhost:5000/Login/Login

        //localhost:5000/Login/Create
        //localhost:5000/Login/Create
        [HttpPost("Login/Create")]
        public IActionResult Create(User NewUser)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine($"-----------{NewUser.Fname} Was Here-----------");
                //CHECK FOR EMAIL UNIQUENESS
                if(dbContext.Users.Any(user => user.Email == NewUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                //CHECK FOR EMAIL UNIQUENESS
                //COMPARE PASSWORDS
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                //COMPARE PASSWORDS
                //MAKE NEW USER
                dbContext.Add(NewUser);
                dbContext.SaveChanges();
                //MAKE NEW USER
                //PUT NEW USERS ID INTO SESSION
                HttpContext.Session.SetInt32("liveUser", NewUser.UserId);
                //PUT NEW USERS ID INTO SESSION
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                Console.WriteLine("-----------User Creation Route-----------");
                Console.WriteLine("-----------//localhost:5000/User/Create-----------");
                Console.WriteLine($"-----------{NewUser.Fname}-----------");
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                return RedirectToAction("Dashboard", "Wedding");
            }
            else
            {
                //STATEMENTS TO DEBUG IF ROUTE IS HIT
                Console.WriteLine("-----------User Creation Validation Fail Route-----------");
                return View("Index");
            }
        }
        //localhost:5000/Login/Create
        //localhost:5000/Login/Create
        //localhost:5000/Login/Logout
        //localhost:5000/Login/Logout
        [Route("Login/Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            //STATEMENTS TO DEBUG IF ROUTE IS HIT
            Console.WriteLine($"-----------Logout Route-----------");
            //CLEAR THE SESSION IE(LOGOUT)
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        } 
        //localhost:5000/Login/Logout
        //localhost:5000/Login/Logout
        //localhost:5000/Login/Success
        //localhost:5000/Login/Success
        [HttpGet("Success")]
        public IActionResult Success()
        {
            User planner = dbContext.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("liveUser"));
            if(planner != null)
            {                
                return View(planner);
            }else{
                ModelState.AddModelError("Email", "Must be logged in!");
                return View("Index");
            }
        }
        //localhost:5000/Login/Success
        //localhost:5000/Login/Success
    }

    //INITIALIZING JSON 'OBJECTS'
    //INITIALIZING JSON 'OBJECTS'
    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes the object to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
    //INITIALIZING JSON 'OBJECTS'
    //INITIALIZING JSON 'OBJECTS'
}
