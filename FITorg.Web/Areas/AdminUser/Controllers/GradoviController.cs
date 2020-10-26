using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.AdminUser.ViewModels;
using FITorg.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


//Drzave kontroler se brine upravljanjem drzava iz admin panela
namespace FITorg.Web.Areas.AdminUser.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("AdminUser")]
    public class GradoviController : Controller
    {
        private MyContext _db;

        public GradoviController(MyContext context)
        {
            _db = context;
        }
        // GET: DrzaveController
        public ActionResult Index()
        {
            GradoviIndexVM model = new GradoviIndexVM()
            {
                Rows = _db.Grad.Select(x => new GradoviIndexVM.Row()
                {
                    GradId = x.GradId,
                    Naziv = x.Naziv,
                    NazivDrzave = x.Drzava.Naziv
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Dodaj()
        {
            GradoviDodajVM vm = new GradoviDodajVM();
            vm.DrzaveItems = _db.Drzava.Select(o => new SelectListItem(o.Naziv, o.DrzavaId.ToString())).ToList();
            return View("Dodaj", vm);
        }

        [HttpPost]
        public IActionResult Dodaj(GradoviDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }
            Grad g = _db.Grad.Where(a => a.GradId == vm.GradId).SingleOrDefault();
            if (g == null)
            {
                Grad a = new Grad
                {
                    GradId = vm.GradId,
                    DrzavaId = vm.DrzavaId,
                    Naziv = vm.Naziv
                };
                _db.Grad.Add(a);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            g.GradId = vm.GradId;
            g.Naziv = vm.Naziv;
            g.DrzavaId = vm.DrzavaId;

            _db.SaveChanges();
            _db.Dispose();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Uredi(int id)
        {
            GradoviDodajVM model = new GradoviDodajVM();
            model.GradId = id;
            model.Naziv = _db.Grad.Where(a => a.GradId == model.GradId).SingleOrDefault().Naziv;
            model.DrzavaId = _db.Grad.Where(a => a.GradId == id).SingleOrDefault().DrzavaId;
            model.DrzaveItems = _db.Drzava.Select(o => new SelectListItem(o.Naziv, o.DrzavaId.ToString())).ToList();

            return View(model);
        }

        public ActionResult Obrisi(int Id)
        {
            Grad x = _db.Grad.Find(Id);
            _db.Grad.Remove(x);
            _db.SaveChanges();
            _db.Dispose();
            return RedirectToAction("Index");
        }
    }
}
