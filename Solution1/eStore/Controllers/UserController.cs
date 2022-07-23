using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eStore.Controllers
{
    public class UserController : Controller
    {
        SalesManagementDBContext db = new SalesManagementDBContext();
        private Member LoginUser()
        {
            int? id = HttpContext.Session.GetInt32("id");
            var member = db.Members.SingleOrDefault(m => m.MemberId == id);
            return member;
        }

        public UserController(SalesManagementDBContext context)
        {
            db = context;
        }
        // GET: UserController
        public ActionResult Index()
        {
            var member = LoginUser();
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        //Get OrderDetail
        public async Task<IActionResult> OrderDetails(int id)
        {
            //var fStoreContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            var fStoreContext = db.OrderDetails.Where(d => d.OrderId == id).OrderBy(d => d.Order.OrderDate);
            ViewData["OrderId"] = id;
            foreach (var detail in fStoreContext)
            {
                detail.Product = db.Products.FindAsync(detail.ProductId).Result;
                detail.Order = db.Orders.FindAsync(detail.OrderId).Result;
            }
            return View(await fStoreContext.ToListAsync());
        }

        //Get Order
        public IActionResult OrderHistory()
        {
            var orders = db.Orders.Where(o => o.MemberId == LoginUser().MemberId).OrderByDescending(o => o.OrderDate);

            return View(orders);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit()
        {
            var member = await db.Members.FindAsync(LoginUser().MemberId);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(member);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        private bool MemberExists(int id)
        {
            return db.Members.Any(e => e.MemberId == id);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
