using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.AdminUser.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace FITorg.Web.Areas.AdminUser.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("AdminUser")]
    public class TreneriController : Controller
    {
        private MyContext _db;

        private readonly UserManager<AppUser> _userMgr;

        public TreneriController(MyContext context, UserManager<AppUser> userManager)
        {
            _db = context;
            _userMgr = userManager;
        }
        // GET: TreneriController
        public ActionResult Index()
        {
            TreneriIndexVM model = new TreneriIndexVM()
            {
                Rows = _db.Trener.Select(x=> new TreneriIndexVM.Row()
                {
                    TrenerId = x.TrenerId,
                    ImePrezime = x.AppUser.Ime +" "+x.AppUser.Prezime,
                    email = x.AppUser.Email,
                    NazivGrada = x.AppUser.Grad.Naziv,
                    NazivDrzave = x.AppUser.Drzava.Naziv,
                    Spol = x.AppUser.Spol.Naziv
                }).ToList()
            };
            return View(model);
        }

       

        // GET: TreneriController/Edit
        public ActionResult Uredi(int id)
        {
            
            Trener t = _db.Trener.Where(t => t.TrenerId == id).Include(a => a.AppUser).
                        Include(x=>x.AppUser.Drzava).FirstOrDefault();
            TreneriUrediVM model = new TreneriUrediVM();

            model.TrenerID = id;
            model.Ime = t.AppUser.Ime;
            model.Prezime = t.AppUser.Prezime;
            model.Email = t.AppUser.Email;
            model.Mob = t.AppUser.PhoneNumber;
            model.DatumRodjenja = t.AppUser.DatumRodjenja;
            model.GradId = t.AppUser.GradId;
            model.Drzava = t.AppUser.Drzava.Naziv;
            model.SpolId = t.AppUser.SpolId;
            model.GradoviItems = _db.Grad.Select(g => new SelectListItem(g.Naziv, g.GradId.ToString())).ToList();
            model.SpolItems = _db.Spol.Select(g => new SelectListItem(g.Naziv, g.SpolId.ToString())).ToList();

            return View(model);
        }

        // POST: TreneriController1/Create
        [HttpPost]
        public async Task<ActionResult> Uredi(TreneriUrediVM vm)
        {
           
            
            
            var userE = await _userMgr.FindByEmailAsync(vm.Email);
            int id = _db.Trener.Where(x => x.TrenerId == vm.TrenerID).Select(x => x.AppUserId).FirstOrDefault();
            if (userE != null && userE.Id!=id)
            {
                TempData["poruka"] = "Email already in use. ";
                return RedirectToAction("Uredi",new{id=vm.TrenerID});
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

            return RedirectToAction(nameof(Index));

        }

        

        // GET: TreneriController1/Delete/5
        public IActionResult Obrisi(int id)
        {
            Trener t = _db.Trener.Find(id);
            AppUser u = _db.Users.Find(t.AppUserId);
            _db.Remove(t);
            _db.Remove(u);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        
        
    }
}
