﻿using Microsoft.AspNetCore.Http;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace eStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SalesManagementDBContext _context;
        IProductRepository productRepository = null ;

        public ProductsController(SalesManagementDBContext context)
        {
            productRepository = new ProductRepository();
            _context = context;
        }

        // GET: Products

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        //public async Task<IActionResult> Index(string searchString, decimal UnitPrice)
        //{
        //    var list = from p in _context.Products
        //               select p;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        list = list.Where(s => s.ProductName.Contains(searchString) && s.UnitPrice <= UnitPrice).OrderByDescending(p => p.ProductName);
        //    }

        //    return View(await list.ToListAsync());
        //}
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitslnStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitslnStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }


        // GET: ProductController/Search/5
        public ActionResult Search()
        {
            string search = Request.Form["txtSearch"];
            string type = Request.Form["type"];
            if (search != null && type.Equals("1"))
            {
                List<Product> listProduct = productRepository.getProductByName(search);
                return View(listProduct);
            }
            else if (search != null && type.Equals("2"))
            {
                List<Product> listProduct = productRepository.getProductByUnitPrice(search);
                return View(listProduct);
            }
            else if (search != null && type.Equals("3"))
            {
                List<Product> listProduct = productRepository.getProductByUnitsSlnStock(search);
                return View(listProduct);
            }
            else
            {
                return View();
            }

        }

    }
}
