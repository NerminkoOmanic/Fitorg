using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.AdminUser.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


//Drzave kontroler se brine upravljanjem drzava iz admin panela
namespace FITorg.Web.Areas.AdminUser.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("AdminUser")]
    public class DrzaveController : Controller
    {
        private MyContext _context;

        public DrzaveController(MyContext context)
        {
            _context = context;
        }
        // GET: DrzaveController
        public ActionResult Index()
        {
            DrzaveIndexVM model = new DrzaveIndexVM()
            {
                Rows = _context.Drzava.Select(x => new DrzaveIndexVM.Row()
                {
                    DrzavaId = x.DrzavaId,
                    Naziv = x.Naziv
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            DrzaveDodajVM model = new DrzaveDodajVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult Dodaj(DrzaveDodajVM vm)
        {
            Drzava r = _context.Drzava.Where(a => a.DrzavaId == vm.DrzavaId).FirstOrDefault();

            if (r == null)
            {
                Drzava a = new Drzava
                {
                    DrzavaId = vm.DrzavaId,
                    Naziv = vm.Naziv
                };
                _context.Drzava.Add(a);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            r.DrzavaId = vm.DrzavaId;
            r.Naziv = vm.Naziv;


            _context.SaveChanges();
            _context.Dispose();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Uredi(int id)
        {
            DrzaveDodajVM model = new DrzaveDodajVM();
            model.DrzavaId = id;
            model.Naziv = _context.Drzava.Where(a => a.DrzavaId == model.DrzavaId).SingleOrDefault().Naziv;
            return View(model);
        }

        public IActionResult Obrisi(int Id)
        {
            Drzava x = _context.Drzava.Find(Id);
            _context.Drzava.Remove(x);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
