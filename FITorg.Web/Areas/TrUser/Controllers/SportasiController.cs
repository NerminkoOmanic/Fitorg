using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Web.Areas.TrUser.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Area za korisnika tipa Trener
namespace FITorg.Web.Areas.TrUser.Controllers
{
    [Authorize(Roles = "Trener")]
    [Area("TrUser")]

    public class SportasiController : Controller
    {
        private MyContext _db;
        // GET: HomeController
        public SportasiController(MyContext context)
        {
            _db = context;
        }
        public ActionResult Index()
        {
            SportasiIndexVM model = new SportasiIndexVM()
            {
                Rows = _db.Sportas.Select(v => new SportasiIndexVM.Row()
                {
                    SportasId = v.SportasId,
                    ImePrezime = v.AppUser.Ime,
                    Email = v.AppUser.Email,
                    Phone = v.AppUser.PhoneNumber,
                    NazivGrada = v.AppUser.Grad.Naziv
                }).ToList()
            };

            return View(model);
        }

        
    }
}
