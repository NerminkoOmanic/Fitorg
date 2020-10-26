using FITorg.Data;
using FITorg.Data.EntityModels;
using FITorg.Web.Areas.AdminUser.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace FITorg.Web.Areas.AdminUser.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("AdminUser")]
    public class AdministratoriController : Controller
    {
        private MyContext _db;

        private readonly UserManager<AppUser> _userMgr;
        private readonly RoleManager<AppRole> _roleMgr;


        public AdministratoriController(MyContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _db = context;

            _userMgr = userManager;
            _roleMgr = roleManager;
        }

        // GET: Controller
        public ActionResult Index()
        {
            AdministratoriIndexVM model = new AdministratoriIndexVM()
            {
                Rows = _db.Administrator.Select(x => new AdministratoriIndexVM.Row()
                {
                    AdminId = x.AdministratorId,
                    ImePrezime = x.AppUser.Ime + " " + x.AppUser.Prezime,
                    Email = x.AppUser.Email,
                    BrojMob = x.AppUser.PhoneNumber,
                    NazivGrada = x.AppUser.Grad.Naziv,
                    NazivDrzave = x.AppUser.Drzava.Naziv,
                    IzjavaOZastitiPodatak = x.IzjavaOZastitiPodatak

                }).ToList()
            };
            
            return View(model);
        }

        public ActionResult Dodaj()
        {
            AdministratoriDodajVM model = new AdministratoriDodajVM();
            model.GradoviItems = _db.Grad.Select(g => new SelectListItem(g.Naziv, g.GradId.ToString())).ToList();
            model.SpolItems = _db.Spol.Select(g => new SelectListItem(g.Naziv, g.SpolId.ToString())).ToList();
            return View(model);
        }

        //    POST: Controller/Create
        [HttpPost]
        public async Task<IActionResult> Snimi(AdministratoriDodajVM vm)
        {
            
            Administrator a = _db.Administrator.Where(o => o.AdministratorId == vm.AdminID).
                Include(b => b.AppUser).Include(x=>x.AppUser.Grad).SingleOrDefault();

            if (a == null)
            {
                var userE = await _userMgr.FindByEmailAsync(vm.Email);
                if (userE != null)
                {
                    TempData["poruka"] = "Email already in use. ";
                    return RedirectToAction("Dodaj");
                }
                bool x = await _roleMgr.RoleExistsAsync("Administrator");

                if (!x)
                {
                    await _roleMgr.CreateAsync(new AppRole()
                    {
                        Name = "Administrator"
                    });
                }
                AppUser appU = new AppUser()
                {

                    UserName = vm.Ime.ToLower() + "." + vm.Prezime.ToLower(),
                    Ime = vm.Ime,
                    Prezime = vm.Prezime,
                    Email = vm.Email,
                    PhoneNumber = vm.BrojMob,
                    DatumRodjenja = vm.DatumRodjenja,
                    GradId = vm.GradId,
                    DrzavaId = _db.Grad.Where(x=>x.GradId == vm.GradId).Select(x=>x.DrzavaId).FirstOrDefault(),
                    SpolId = vm.SpolId
                };

                await _userMgr.CreateAsync(appU, vm.Password);
                await _userMgr.AddToRoleAsync(appU, "Administrator");

                Administrator admin = new Administrator
                {
                    AppUser = appU,
                    AppUserId = appU.Id,
                    IzjavaOZastitiPodatak = vm.IzjavaOZastitiPodatak
                };

                _db.Administrator.Add(admin);
            }
            else
            {
                var userE = _db.Users.Where(x=>x.Email==vm.Email).FirstOrDefault();
                int id = _db.Administrator.Where(x => x.AdministratorId == vm.AdminID).Select(x => x.AppUserId)
                    .FirstOrDefault();
                if (userE != null && userE.Id!=id)
                {
                    TempData["poruka"] = "Email already in use. ";
                    return RedirectToAction("Uredi",new{id=a.AdministratorId});
                }

                a.AppUser.Ime = vm.Ime;
                a.AppUser.Prezime = vm.Prezime;
                a.AppUser.Email = vm.Email;
                a.AppUser.PhoneNumber = vm.BrojMob;
                a.AppUser.DatumRodjenja = vm.DatumRodjenja;
                a.AppUser.GradId = vm.GradId;
                a.AppUser.DrzavaId = _db.Grad.Where(x=>x.GradId == vm.GradId).Select(x=>x.DrzavaId).FirstOrDefault();
                a.AppUser.SpolId = vm.SpolId;
                a.IzjavaOZastitiPodatak = vm.IzjavaOZastitiPodatak; 
            }
           

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //    GET: Controller/Edit/5
        public ActionResult Uredi(int id)
        {
            Administrator admin = _db.Administrator.Where(a => a.AdministratorId == id).Include(b => b.AppUser).SingleOrDefault();
            AdministratoriDodajVM model = new AdministratoriDodajVM();
            model.AdminID = admin.AdministratorId;
            model.Ime = admin.AppUser.Ime;
            model.Prezime = admin.AppUser.Prezime;
            model.Email = admin.AppUser.Email;
            model.BrojMob = admin.AppUser.PhoneNumber;
            model.DatumRodjenja = admin.AppUser.DatumRodjenja;
            model.GradId = admin.AppUser.GradId;
            model.SpolId = admin.AppUser.SpolId;
            model.IzjavaOZastitiPodatak = admin.IzjavaOZastitiPodatak;
            model.GradoviItems = _db.Grad.Select(g => new SelectListItem(g.Naziv, g.GradId.ToString())).ToList();
            model.SpolItems = _db.Spol.Select(g => new SelectListItem(g.Naziv, g.SpolId.ToString())).ToList();


            return View(model);
        }

        

        public ActionResult Obrisi(int id)
        {
            Administrator a = _db.Administrator.Find(id);
            AppUser u = _db.Users.Find(a.AppUserId);
            _db.Remove(a);
            _db.Remove(u);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }

}
