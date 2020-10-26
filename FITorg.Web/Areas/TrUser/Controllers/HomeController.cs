using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.AdminUser.ViewModels;
using FITorg.Web.Areas.TrUser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

//Area za korisnika tipa Trener
namespace FITorg.Web.Areas.TrUser.Controllers
{
    [Authorize(Roles = "Trener")]
    [Area("TrUser")]

    public class HomeController : Controller
    {
        
        private MyContext _db;

        public HomeController(MyContext context)
        {
            _db = context;
        }
        
        public ActionResult Index(int id)
        {
            TreneriPrikaziVM model = new TreneriPrikaziVM();
            Trener t = _db.Trener.Where(x => x.AppUserId == id).
                Include(y=>y.AppUser).
                Include(y=>y.AppUser.Grad).
                Include(y=>y.AppUser.Drzava).
                Include(y=>y.AppUser.Spol).FirstOrDefault();
            model.TrenerID = t.TrenerId;
            model.Ime = t.AppUser.Ime;
            model.Prezime = t.AppUser.Prezime;
            model.DatumRodjenja = t.AppUser.DatumRodjenja;
            model.Email = t.AppUser.Email;
            model.Mob = t.AppUser.PhoneNumber;
            model.GradNaziv = t.AppUser.Grad.Naziv;
            //model.GradNaziv = _db.Grad.Where(g => g.GradId == t.AppUser.GradId).FirstOrDefault().Naziv; pitanje memorije ???
            model.DrzavaNaziv = t.AppUser.Drzava.Naziv; 
            model.SpolNaziv = t.AppUser.Spol.Naziv;

            return View(model);
        }



       
        public ActionResult Uredi(int id)
        {   TreneriUrediVM model = new TreneriUrediVM();
            Trener t = _db.Trener.Include(a => a.AppUser).Include(x=>x.AppUser.Drzava).Where(t => t.TrenerId == id).FirstOrDefault();

            model.TrenerID = t.TrenerId;
            model.Ime = t.AppUser.Ime;
            model.Prezime = t.AppUser.Prezime;
            model.DatumRodjenja = t.AppUser.DatumRodjenja;
            model.Email = t.AppUser.Email;
            model.Mob = t.AppUser.PhoneNumber;
            model.GradId = t.AppUser.GradId;
            model.Drzava = t.AppUser.Drzava.Naziv;
            model.SpolId = t.AppUser.SpolId;
            model.GradoviItems = _db.Grad.Select(a => new SelectListItem(a.Naziv, a.GradId.ToString())).ToList();
            model.SpolItems = _db.Spol.Select(a => new SelectListItem(a.Naziv, a.SpolId.ToString())).ToList();


            return View(model);
        }

        //POST: HomeController/Uredi
        [HttpPost]
        public ActionResult Uredi(TreneriUrediVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Uredi",vm);
            }
            Trener t = _db.Trener.Where(d => d.TrenerId == vm.TrenerID).Include(a =>a.AppUser).SingleOrDefault();

            t.AppUser.Ime = vm.Ime;
            t.AppUser.Prezime = vm.Prezime;
            t.AppUser.Email = vm.Email;
            t.AppUser.PhoneNumber = vm.Mob;
            t.AppUser.DatumRodjenja = vm.DatumRodjenja;
            t.AppUser.GradId = vm.GradId;
            t.AppUser.DrzavaId = _db.Grad.Where(x => x.GradId == vm.GradId).Select(x => x.DrzavaId).FirstOrDefault();
            t.AppUser.SpolId = vm.SpolId;

            _db.SaveChanges();
            _db.Dispose();

            return RedirectToAction(nameof(Index),new{id=t.AppUserId});
        }

        
    }
}
