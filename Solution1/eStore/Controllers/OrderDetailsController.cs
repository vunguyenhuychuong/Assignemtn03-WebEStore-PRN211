using Microsoft.AspNetCore.Mvc;
using BussinessObject.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using DataAccess.Repository;
namespace eStore.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly SalesManagementDBContext _context;
        IOrderDetailRepository orderDetailRepository;

        public OrderDetailsController(SalesManagementDBContext context)
        {
            _context = context;
            orderDetailRepository = new OrderDetailRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: OrderDetails
        public async Task<IActionResult> OrderDetails(int id)
        {
            var fStoreContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            //var fStoreContext = _context.OrderDetails.Where(d => d.OrderId == id).OrderBy(d => d.Order.OrderDate);
            ViewData["OrderId"] = id;
            foreach (var detail in fStoreContext)
            {
                detail.Product = _context.Products.FindAsync(detail.ProductId).Result;
                detail.Order = _context.Orders.FindAsync(detail.OrderId).Result;
            }
            return View(await fStoreContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create(int id)
        {
            ViewData["OrderId"] = new SelectList(_context.Orders.Where(o => o.OrderId == id), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OrderDetails), new {id = orderDetail.OrderId});
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("OrderDetails", "OrderDetails", id = orderDetail.OrderId);
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            //var orderDetail = this._context.OrderDetails.Find(id);
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            //this._context.SaveChanges();
            return RedirectToAction(nameof(OrderDetails));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}
