using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FITorg.Data;
using FITorg.Data.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FITorg.Data.Models;
using FITorg.Web.Areas.AdminUser.ViewModels;
using FITorg.Web.Models;
using FITorg.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FITorg.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext _db;

        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signInMgr;
        private readonly RoleManager<AppRole> _roleMgr;
        private readonly UrlEncoder _urlEncoder;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,
            UrlEncoder urlEncoder, ILogger<AccountController> logger, MyContext db)
        {
            _userMgr = userManager;
            _signInMgr = signInManager;
            _roleMgr = roleManager;
            _urlEncoder = urlEncoder;


            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }

        [HttpPost]
        async public Task<IActionResult> Login(LoginVM input)
        {
            AppUser user = _db.Users.FirstOrDefault(x => x.Email == input.Email);
            if (user == null)
            {
                TempData["poruka"] = "Ne postoji korisnik sa unesenim e-mailom !!!";
                return RedirectToAction(nameof(Index));
            }

            var result = await _signInMgr.PasswordSignInAsync(user, input.Password, false, false);

            if (result.Succeeded)
            {
                var userRole =  _userMgr.GetRolesAsync(user).Result.Single();

                if (userRole == "Administrator")
                    return RedirectToAction("Index", "Home", new {area = "AdminUser"});
                else if (userRole == "Trener")
                    return RedirectToAction("Index", "Home", new {id=user.Id ,area = "TrUser"});
            }
            else if (result.RequiresTwoFactor)
            {
                return LocalRedirect("/Identity/Account/LoginWith2fa");
            }
            TempData["poruka"] = "Wrong password !!!";
            return RedirectToAction(nameof(Index));


        }
        public IActionResult Register()
        {
            if (!_db.Administrator.Any())
            {
                AdministratoriDodajVM model = new AdministratoriDodajVM()
                {
                    GradoviItems = _db.Grad.Select(o => new SelectListItem(o.Naziv, o.GradId.ToString())).ToList(),
                    SpolItems = _db.Spol.Select(o => new SelectListItem(o.Naziv, o.SpolId.ToString())).ToList()
                };
                return View("RegAdmina",model);
            }
            else
            {
                TrenerRegVM model = new TrenerRegVM
                {
                    GradoviItems = _db.Grad.Select(o => new SelectListItem(o.Naziv, o.GradId.ToString())).ToList(),
                    SpolItems = _db.Spol.Select(o => new SelectListItem(o.Naziv, o.SpolId.ToString())).ToList()
                };

                return View("Register",model);  
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> RegisterT(TrenerRegVM input)
        {
            

                var userE = await _userMgr.FindByEmailAsync(input.Email);
                if (userE != null)
                {
                    TempData["poruka"] = "Email already in use. ";
                    return View("Register",input);
                }
                bool x = await _roleMgr.RoleExistsAsync("Trener");

                if (!x)
                {
                    await _roleMgr.CreateAsync(new AppRole()
                    {
                        Name = "Trener"
                    });
                }

                AppUser user = new AppUser()
                {
                    UserName = input.Ime.ToLower() + "." + input.Prezime.ToLower(),
                    Ime = input.Ime,
                    Prezime = input.Prezime,
                    SpolId = input.SpolId,
                    Email = input.Email,
                    DrzavaId = _db.Grad.Where(x=>x.GradId == input.GradId).Select(x=>x.DrzavaId).FirstOrDefault(),
                    GradId = input.GradId,
                    DatumRodjenja = input.DatumRodjenja
                };

                await _userMgr.CreateAsync(user, input.Password);
                await _userMgr.AddToRoleAsync(user, "Trener");


                Trener t = new Trener
                {
                    AppUserId = user.Id
                };
            
                _db.Trener.Add(t);
                _db.SaveChanges();
           
                return RedirectToAction("Index");
            
           
        }

        [HttpPost]
        public async Task<IActionResult> RegisterA(AdministratoriDodajVM input)
        {
            await _roleMgr.CreateAsync(new AppRole()
            {
                Name = "Administrator"
            });

            AppUser appU = new AppUser()
            {
                UserName = input.Ime.ToLower() + "." + input.Prezime.ToLower(),
                Ime = input.Ime,
                Prezime = input.Prezime,
                Email = input.Email,
                PhoneNumber = input.BrojMob,
                DatumRodjenja = input.DatumRodjenja,
                GradId = input.GradId,
                DrzavaId = _db.Grad.Where(x=>x.GradId == input.GradId).Select(x=>x.DrzavaId).FirstOrDefault(),
                SpolId = input.SpolId
            };

            await _userMgr.CreateAsync(appU, input.Password);
            await _db.Users.AddAsync(appU);
            await _db.SaveChangesAsync();
            await _userMgr.AddToRoleAsync(appU, "Administrator");
            
            
            Administrator admin = new Administrator
            {
                AppUserId = appU.Id,
                IzjavaOZastitiPodatak = input.IzjavaOZastitiPodatak
            };

            await _db.Administrator.AddAsync(admin);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Logout()
        {
            await _signInMgr.SignOutAsync();

            TempData["poruka"] = "Uspjesno ste se odlogovali.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangePassword(string id)
        {
            ChangePasswordVM model = new ChangePasswordVM();
            model.UserId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM input)
        {

            AppUser user = _db.Users.Find(int.Parse(input.UserId));
            bool result = await _userMgr.CheckPasswordAsync(user, input.StariPW);
            if (!result)
            {
                TempData["poruka"] = "Unesen je pogresan stari password. ";
                return RedirectToAction(nameof(ChangePassword), new {id = input.UserId});
            }

            await _userMgr.ChangePasswordAsync(user, input.StariPW, input.NoviPW);
            await _db.SaveChangesAsync();
            var userRole = _userMgr.GetRolesAsync(user).Result.Single();

            if (userRole == "Administrator")
                return RedirectToAction("Index", "Home", new {area = "AdminUser"});
            else
                return RedirectToAction("Index", "Home", new {id=user.Id ,area = "TrUser"});
        }

        #region Helpers
        public IActionResult AccessDenied()
        {
            TempData["poruka"] = "Neovlasten pristup.";
            
            return RedirectToAction(nameof(Index));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
        #endregion
    }   
}
