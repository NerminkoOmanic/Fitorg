using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.TrUser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Area za korisnika tipa Trener
namespace FITorg.Web.Areas.TrUser.Controllers
{
    [Authorize(Roles = "Trener")]
    [Area("TrUser")]

    public class VjezbeController : Controller
    {
        private MyContext _db;
        // GET: HomeController
        public VjezbeController(MyContext context)
        {
            _db = context;
        }
        public ActionResult Index(string idStr,int idInt)
        {
            if (idStr != null)
            {
                Trener t = _db.Trener.Where(x => x.AppUserId == int.Parse(idStr)).FirstOrDefault();

                VjezbeIndexVM model = new VjezbeIndexVM()
                {
                    Rows = _db.Vjezba.Where(a => a.TrenerId == t.TrenerId).Select(v => new VjezbeIndexVM.Row()
                    {
                        VjezbaId = v.VjezbaId,
                        Naziv = v.Naziv,
                        videoUrl = v.VideoUrl
                    }).ToList()
                };
                return View(model);
            }
            else
            {
                VjezbeIndexVM model = new VjezbeIndexVM()
                {
                    Rows = _db.Vjezba.Where(a => a.TrenerId == idInt).Select(v => new VjezbeIndexVM.Row()
                    {
                        VjezbaId = v.VjezbaId,
                        Naziv = v.Naziv,
                        videoUrl = v.VideoUrl
                    }).ToList()
                };
                return View(model);
            }
        }


        // GET: HomeController/Dodaj (prima trener id)
        public ActionResult Dodaj(string id)
        {
            VjezbeDodajVM model = new VjezbeDodajVM();
            model.AppUserId=int.Parse(id);
            return View(model);
        }

        // POST: HomeController/Dodaj
        [HttpPost]
        public ActionResult Dodaj(VjezbeDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Vjezba v = new Vjezba
            {
                Naziv = vm.Naziv,
                TrenerId = _db.Trener.Where(x=>x.AppUserId == vm.AppUserId).Select(x=>x.TrenerId).FirstOrDefault(),
                VideoUrl = vm.videoUrl
            };
            _db.Add(v);
            _db.SaveChanges();
            _db.Dispose();

            return RedirectToAction(nameof(Index),new{idInt=v.TrenerId});
            
        }

        // GET: HomeController/Uredi
        public ActionResult Uredi(int id)
        {
            VjezbeDodajVM model =new VjezbeDodajVM();
            Vjezba v = _db.Vjezba.Find(id);
            model.Naziv = v.Naziv;
            model.videoUrl = v.VideoUrl;
            model.VjezbaId = v.VjezbaId;
            return View(model);
        }

        // POST: HomeController/Uredi
        [HttpPost]
        public ActionResult Uredi(VjezbeDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Vjezba v = _db.Vjezba.Find(vm.VjezbaId);
            v.Naziv = vm.Naziv;
            v.VideoUrl = vm.videoUrl;
            _db.SaveChanges();
            _db.Dispose();
            return RedirectToAction(nameof(Index),new{idInt=v.TrenerId});
          
        }

        // GET: HomeController/Delete/5
        public ActionResult Obrisi(int id)
        {
            int trenerId = _db.Vjezba.Where(x => x.VjezbaId == id).Select(x => x.TrenerId).FirstOrDefault();
            Vjezba v = _db.Vjezba.Find(id);
            _db.Remove(v);
            _db.SaveChanges();
            _db.Dispose();
            return RedirectToAction(nameof(Index),new{trenerId});

        }

        public ActionResult PrikaziVideoPV(string url)
        {
            string pomocni = "watch?v=";
            string code = url.Split(pomocni, StringSplitOptions.None).Last();

            TempData["urlcode"] = code;
            return PartialView();
        }
    }
}
