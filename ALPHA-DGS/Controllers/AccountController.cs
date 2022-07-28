using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALPHA_DGS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ALPHA_DGS.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }


        /// Home Pagina voor Gebruikers

        public IActionResult Index()
        {
            return View();
        }


        /// Login Scherm + GET Method
        [HttpGet]
        public IActionResult Login(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }


        /// Login Scherm + POST Method (hoe je de data post naar de App
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnurl);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ongeldig Login.");
                    return View(model);

                }

            }


            return View(model);
        }
















        /// Registratie Scherm + GET Method (NIET IN GEBRUIK)

        [HttpGet]
        public async Task<IActionResult> Register(string returnurl = null)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                //create roles

                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Gebruiker"));
                await _roleManager.CreateAsync(new IdentityRole("Tiertary"));
            }

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Gebruiker",
                Text = "Gebruiker"
            });




            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerviewmodel = new RegisterViewModel()
            {
                RoleList = listItems


            };
            return View(registerviewmodel);
        }

        /// Registratie Scherm + POST Method (NIET IN GEBRUIK)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Gebruiker");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnurl);
                }

                AddErrors(result);


            }

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Gebruiker",
                Text = "Gebruiker"
            });

            model.RoleList = listItems;
            return View(model);
        }

        /// Uitloggen functie 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff(RegisterViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }




        /// Wachtwoord vergeten functie  (DEFUNCT door SSO)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        /// Wachtwoord vergeten functie  (DEFUNCT door SSO)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View(model);
        }


        /// Wachtwoord vergeten functie  (DEFUNCT door SSO)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        /// Wachtwoord reset functie  (DEFUNCT door SSO)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }


        /// Wachtwoord reset functie  (DEFUNCT door SSO)
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                AddErrors(result);
            }

            return View();
        }

        /// Wachtwoord reset functie  (DEFUNCT door SSO)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }



        /// Single Sign On Functie  (SSO via open-id aan een IdentityServer)
        public IActionResult ExternalLogin(string provider, string returnurl = null)

        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnurl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return Challenge(properties, provider);
        }


        /// Single Sign On Functie  (SSO via open-id aan een IdentityServer)
        public async Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {
            returnurl = returnurl ?? Url.Content("~/");

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }



            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }


            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {

                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnurl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnurl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            }

        }

        /// Single Sign On Functie  (SSO via open-id aan een IdentityServer)
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnurl = null)
        {
            returnurl = returnurl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                //get the info about the user from external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Gebruiker"); // DIT IS DE ROLE DAT JE KRIJGT ALS SSO INLOG DOOR GAAT (SINGLE SIGN ON)
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnurl);
                    }
                }
                AddErrors(result);
            }
            ViewData["ReturnUrl"] = returnurl;
            return View(model);
        }

        /// Single Sign On Functie  (SSO via open-id aan een IdentityServer)
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


        }
    }
}