using Microsoft.AspNetCore.Mvc;
using BussinessObject.Models;
using DataAccess.Repository;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace eStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly SalesManagementDBContext _context;
        IOrderRepository orderRepository;

        public OrdersController(SalesManagementDBContext context)
        {
            _context = context;
            orderRepository = new OrderRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Orders
        public async Task<IActionResult> ViewOrders(DateTime startDate, DateTime endDate)
        {
            var saleManagementContext = _context.Orders.Include(o => o.Member);
            try
            {
                var temp = _context.Orders.Include(o => o.Member).Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);
                return View(await temp.ToListAsync());
            }
            catch
            {
                
            }
            return View(await saleManagementContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Member)
                //.Include(o => o.MemberId == id)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            //return View(order);
            return RedirectToAction("OrderDetails.cshtml", order.OrderId);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewOrders));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", order.MemberId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", order.MemberId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewOrders));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", order.MemberId);
            return View(order);
        }

        //GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var order = orderRepository.GetOrderById(id);
            if(order == null)
            {
                return NotFound();
            }
            orderRepository.Remove(id);
            return RedirectToAction(nameof(ViewOrders));
        }
        //public ActionResult Delete(int id)
        //{
        //    var order = orderRepository.GetOrderById(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    orderRepository.Remove(id);
        //    return RedirectToAction(nameof(ViewOrders));
        //}

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, IFormCollection collection)
        {
            //var order = await _context.Orders.FindAsync(id);
            //_context.Orders.Remove(order);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(ViewOrders));
            try
            {
                return RedirectToAction(nameof(ViewOrders));
            }
            catch
            {
                return View();
            }
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(ViewOrders));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
