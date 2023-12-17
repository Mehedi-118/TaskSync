using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model;
using PTSL.Ovidhan.Web.Core.Services.Implementation.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Core.Controllers.SystemUser
{
    [SessionAuthorize]

    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(HttpHelper httpHelper)
        {
            _userService = new UserService(httpHelper);
        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {

            LoginVM model = new LoginVM();
            return View(model);
        }



        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            (ExecutionState executionState, LoginResultVM entity, string message) result = _userService.UserLogin(model);

            ViewBag.ErrorMsg = "Username or Password Invalid !";

            return View(model);
        }

        // GET: UrerList
        [AllowAnonymous]
        public ActionResult UserLists()
        {
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse = _userService.List();
            return View(returnResponse.entity);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Registration()
        {
            var userRegistrationVM = new UserRegisterModel();
            return View(userRegistrationVM);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(UserRegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    (ExecutionState executionState, UserVM entity, string message) returnResponse = _userService.Create(model);
                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Registration");
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }


        public ActionResult UserEdit(int? id)
        {
            if (id == null)
            {
                return Ok();
            }

            (ExecutionState executionState, UserVM entity, string message) result = _userService.GetById(id);

            return View(result.entity);
        }

        // POST: Account/UserEdit/5
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(int id, UserVM entity)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(AccountController.UserLists), "Account");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;

                    (ExecutionState executionState, UserVM entity, string message) returnResponse = _userService.Update(entity);
                    ////					//Session["Message"] = returnResponse.message;
                    //					//Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("BeneficiaryUser");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: Account/UserDelete/5
        [AllowAnonymous]
        public JsonResult UserDelete(int id)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse = _userService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "User deleted successfully.";
            }
            return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }



        
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var user = await UserManager.FindByNameAsync(model.Email);
        //		if (user == null || !await UserManager.IsEmailConfirmedAsync(user.Id))
        //		{
        //			// Don't reveal that the user does not exist or is not confirmed
        //			return View("ForgotPasswordConfirmation");
        //		}

        //		// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //		// Send an email with this link
        //		// string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //		// var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //		// await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //		// return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //	}

        //	// If we got this far, something failed, redisplay form
        //	return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View(model);
        //	}
        //	var user = await UserManager.FindByNameAsync(model.Email);
        //	if (user == null)
        //	{
        //		// Don't reveal that the user does not exist
        //		return RedirectToAction("ResetPasswordConfirmation", "Account");
        //	}
        //	var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //	if (result.Succeeded)
        //	{
        //		return RedirectToAction("ResetPasswordConfirmation", "Account");
        //	}
        //	AddErrors(result);
        //	return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //	// Request a redirect to the external login provider
        //	return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //	var userId = await SignInManager.GetVerifiedUserIdAsync();
        //	if (userId == null)
        //	{
        //		return View("Error");
        //	}
        //	var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //	var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //	return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View();
        //	}

        //	// Generate the token and send it
        //	if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //	{
        //		return View("Error");
        //	}
        //	return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        //}

        //
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //	var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //	if (loginInfo == null)
        //	{
        //		return RedirectToAction("Login");
        //	}

        //	// Sign in the user with this external login provider if the user already has a login
        //	var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //	switch (result)
        //	{
        //		case SignInStatus.Success:
        //			return RedirectToLocal(returnUrl);
        //		case SignInStatus.LockedOut:
        //			return View("Lockout");
        //		case SignInStatus.RequiresVerification:
        //			return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //		case SignInStatus.Failure:
        //		default:
        //			// If the user does not have an account, then prompt the user to create an account
        //			ViewBag.ReturnUrl = returnUrl;
        //			ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //			return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //	}
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //	if (User.Identity.IsAuthenticated)
        //	{
        //		return RedirectToAction("Index", "Manage");
        //	}

        //	if (ModelState.IsValid)
        //	{
        //		// Get the information about the user from the external login provider
        //		var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //		if (info == null)
        //		{
        //			return View("ExternalLoginFailure");
        //		}
        //		var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //		var result = await UserManager.CreateAsync(user);
        //		if (result.Succeeded)
        //		{
        //			result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //			if (result.Succeeded)
        //			{
        //				await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //				return RedirectToLocal(returnUrl);
        //			}
        //		}
        //		AddErrors(result);
        //	}

        //	ViewBag.ReturnUrl = returnUrl;
        //	return View(model);
        //}

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            //Session["UserEmail"] = string.Empty;
            //MySession.Current.Token = string.Empty;
            //Session.Abandon();
            //Session.Clear();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            ////return Json(true,SerializerOption.Default);
            //return RedirectToAction("Login", "Account");

            //Session["UserEmail"] = string.Empty;
            HttpContext.Session.Clear();

            //MySession.Current.Token = string.Empty;
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        ////
        //// POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    //Session["UserEmail"] = string.Empty;
        //	HttpContext.Session.Clear();

        //    //MySession.Current.Token = string.Empty;
        //    //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Login", "Account");
        //}

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //protected override void Dispose(bool disposing)
        //{
        //	if (disposing)
        //	{
        //		if (_userManager != null)
        //		{
        //			_userManager.Dispose();
        //			_userManager = null;
        //		}

        //		if (_signInManager != null)
        //		{
        //			_signInManager.Dispose();
        //			_signInManager = null;
        //		}
        //	}

        //	base.Dispose(disposing);
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //	get
        //	{
        //		return HttpContext.GetOwinContext().Authentication;
        //	}
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //	foreach (var error in result.Errors)
        //	{
        //		ModelState.AddModelError("", error);
        //	}
        //}

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //	if (Url.IsLocalUrl(returnUrl))
        //	{
        //		return Redirect(returnUrl);
        //	}
        //	return RedirectToAction("Index", "Home");
        //}

        //internal class ChallengeResult : HttpUnauthorizedResult
        //{
        //	public ChallengeResult(string provider, string redirectUri)
        //		: this(provider, redirectUri, null)
        //	{
        //	}

        //	public ChallengeResult(string provider, string redirectUri, string userId)
        //	{
        //		LoginProvider = provider;
        //		RedirectUri = redirectUri;
        //		UserId = userId;
        //	}

        //	public string LoginProvider { get; set; }
        //	public string RedirectUri { get; set; }
        //	public string UserId { get; set; }

        //	public override void ExecuteResult(ControllerContext context)
        //	{
        //		var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //		if (UserId != null)
        //		{
        //			properties.Dictionary[XsrfKey] = UserId;
        //		}
        //		context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //	}
        //}
        #endregion
    }
}
