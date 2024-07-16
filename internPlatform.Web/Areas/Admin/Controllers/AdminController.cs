using internPlatform.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers    
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }// GET: Admin


        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.GetUserName();
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {

                bool adminRole = UserManager.IsInRole(user.Id, "Admin");
                bool userRole = UserManager.IsInRole(user.Id, "User");

                if (adminRole)
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}