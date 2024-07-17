//// Controllers/InvoiceController.cs
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SimpleASPLogin;
//using SimpleASPLogin.Filters;
//using SimpleASPLogin.Models;

//namespace SimpleASPLogin.Controllers
//{
//    [SessionAuthorize]
//    public class InvoiceController : BaseController
//    {
//        private readonly SimpleDbContext _dbContext;

//        public InvoiceController(SimpleDbContext context)
//        {
//            _dbContext = context;
//        }

//        [HttpGet]
//        public IActionResult CreateInvoice()
//        {
//            ViewBag.Company = _dbContext.Company.ToList();
//            return View();
//        }

//        public IActionResult CreateInvoice(Invoice invoice)
//        {

//            ViewBag.Company = _dbContext.Company.ToList();
//            return CreateInvoice(invoice, ViewBag);
//        }

//        [HttpPost]
//        public IActionResult CreateInvoice(Invoice invoice, dynamic viewBag)
//        {
//            ViewBag.Company = _dbContext.Company.ToList();
//            invoice.SubTotal = invoice.RentalEntries.Sum(e => e.Hire);
//            invoice.CGST = invoice.SubTotal * 0.06m;
//            invoice.SGST = invoice.SubTotal * 0.06m;
//            invoice.Total = invoice.SubTotal + invoice.CGST + invoice.SGST;
//            if (ModelState.IsValid)
//            {
//                _dbContext.Invoice.Add(invoice);
//                _dbContext.SaveChanges();
//                //return RedirectToAction("InvoiceList");
//                return RedirectToAction("InvoiceA4", new { id = invoice.Id });
//            }

//            viewBag.Company = _dbContext.Company.ToList();
//            return View(invoice);
//        }

//        [HttpGet]
//        public IActionResult InvoiceA4(int id)
//        {
//            ViewBag.Company = _dbContext.Company.ToList();
//            var invoice = _dbContext.Invoice
//                .Include(i => i.RentalEntries)
//                .FirstOrDefault(i => i.Id == id);

//            if (invoice == null)
//            {
//                return NotFound();
//            }

//            return View(invoice);
//        }

//        [HttpGet]
//        public IActionResult InvoiceList()
//        {
//            var invoices = _dbContext.Invoice.ToList();
//            return View(invoices);
//        }
//    }
//}