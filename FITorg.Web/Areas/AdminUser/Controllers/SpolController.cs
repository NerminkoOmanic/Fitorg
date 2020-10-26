using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.AdminUser.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


//Spol kontroler se brine upravljanjem spolova iz admin panela
namespace FITorg.Web.Areas.AdminUser.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("AdminUser")]
    public class SpolController : Controller
    {
        private MyContext _db;

        public SpolController(MyContext context)
        {
            _db = context;
        }
        // GET: DrzaveController
        public ActionResult Index()
        {
            SpolIndexVM model = new SpolIndexVM()
            {
                Rows = _db.Spol.Select(s=>new SpolIndexVM.Row()
                {
                    Naziv = s.Naziv,
                    SpolId = s.SpolId
                }).ToList()
            };
            
            return View(model);
        }

        public ActionResult Dodaj()
        {
            SpolDodajVM model = new SpolDodajVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult Dodaj(SpolDodajVM vm)
        {
            Spol s = _db.Spol.Where(a => a.SpolId == vm.SpolId).FirstOrDefault();

            if (s == null)
            {
                Spol a = new Spol
                {
                    SpolId = vm.SpolId,
                    Naziv = vm.Naziv
                };
                _db.Spol.Add(a);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            s.SpolId = vm.SpolId;
            s.Naziv = vm.Naziv;


            _db.SaveChanges();
            _db.Dispose();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Uredi(int id)
        {
            SpolDodajVM model = new SpolDodajVM();
            model.SpolId = id;
            model.Naziv = _db.Spol.Where(a => a.SpolId == model.SpolId).SingleOrDefault().Naziv;
            return View(model);
        }

        public IActionResult Obrisi(int id)
        {
            Spol x = _db.Spol.Find(id);
            _db.Spol.Remove(x);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
