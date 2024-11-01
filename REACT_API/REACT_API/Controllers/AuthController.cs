using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REACT_API.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using REACT_API.Utils;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using REACT_API.CommonClasses;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.DataProtection;

namespace REACT_API.Controllers
{
    // Controllers/AuthController.cs

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MAIN01DbContext _context;
        private readonly EmailHelper _emailHelper;

        public AuthController(MAIN01DbContext context, EmailHelper emailHelper)
        {
            _context = context;
            _emailHelper = emailHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserAuthentication([FromBody] user_info request)
        {
            // var test = HttpContext.Session.GetString("UIV");
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { status = false, info = "Please provide complete information" });
            }

            try
            {
                // Encrypt the password using a method of your choice
                string encryptedPassword = AesOperation.EncryptString(request.Password);

                var user = await _context.VwUserInfos
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == encryptedPassword);

                if (user == null)
                {
                    return BadRequest(new { status = false, info = "Authentication failed" });
                }

                if ((bool)!user.Varified)
                {
                    return BadRequest(new { status = false, info = "Account not verified" });
                }

                if (request.UserIp != user.UserIp)
                {
                    // Send confirmation email logic
                    bool emailSent = SendConfirmationEmail(user.FirstName, user.Email, request.UserIp);
                    if (!emailSent)
                    {
                        return BadRequest(new { status = false, info = "Authentication failed" });
                    }
                }

                // Update user login status and return response
                user.IsLogin = true; // Update user login status
                await _context.SaveChangesAsync();

                var response = new
                {
                    status = true,
                    info = $"{user.FirstName} {user.LastName}",
                    userid = user.SeqNum.ToString(),
                    email = user.Email.ToString(),
                    redirect = Url.Action("Dashboard", "Pages") // Adjust according to your routing
                };

                // Set cookies
                Response.Cookies.Append("UEV", AesOperation.EncryptString(user.Email + Guid.NewGuid()), new CookieOptions { MaxAge = TimeSpan.FromDays(7), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None });
                Response.Cookies.Append("UIV", AesOperation.EncryptString(user.SeqNum.ToString() + Guid.NewGuid()), new CookieOptions { MaxAge = TimeSpan.FromDays(7), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None });
                Response.Cookies.Append("UFLN", AesOperation.EncryptString($"{user.FirstName} {user.LastName}" + Guid.NewGuid()), new CookieOptions { MaxAge = TimeSpan.FromDays(7), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None });
                Response.Cookies.Append("URLE", AesOperation.EncryptString(user.RoleName + Guid.NewGuid()), new CookieOptions { MaxAge = TimeSpan.FromDays(7), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None });

                HttpContext.Session.SetString("UEV", user.Email.ToString());
                HttpContext.Session.SetString("UIV", user.SeqNum.ToString());
                HttpContext.Session.SetString("UFLN", $"{user.FirstName} {user.LastName}");
                HttpContext.Session.SetString("URLE", user.RoleName.ToString());

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, info = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("dashboard")]
        public IActionResult GetUserDashboard()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get the user ID
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value; // Get the user email
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            // This endpoint requires a valid JWT to access
            return Ok(new
            {
                info = $"Welcome to your dashboard, {userName}",
                userId = userId,
                userEmail = userEmail,
                userRole = userRole,
                userName = userName
            });
        }
        [HttpPost("login1")]
        public async Task<IActionResult> UserAuthentication1([FromBody] user_info request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { status = false, info = "Please provide complete information" });
            }

            var expiredCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddDays(-1), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None };

            if (!string.IsNullOrEmpty(Request.Cookies["Access_Token"]))
            {
                Response.Cookies.Append("Access_Token", "", expiredCookieOptions);
            }

            try
            {
                // Encrypt password
                string encryptedPassword = AesOperation.EncryptString(request.Password);

                var user = await _context.VwUserInfos
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == encryptedPassword);

                if (user == null)
                {
                    return BadRequest(new { status = false, info = "Authentication failed" });
                }

                if ((bool)!user.Varified)
                {
                    return BadRequest(new { status = false, info = "Account not verified" });
                }

                if (request.UserIp != user.UserIp)
                {
                    // Send confirmation email logic
                    bool emailSent = SendConfirmationEmail(user.FirstName, user.Email, request.UserIp);
                    if (!emailSent)
                    {
                        return BadRequest(new { status = false, info = "Authentication failed" });
                    }
                }

                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("YourVeryLongSecureSecretKeyHere1234567890!@#$"); // Use the same key as in Startup.cs

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.SeqNum.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Role, user.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = "YourAppIssuer",
                    Audience = "YourAppAudience",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                Response.Cookies.Append("Access_Token", jwtToken, new CookieOptions { MaxAge = TimeSpan.FromDays(7), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None });

                return Ok(new
                {
                    status = true,
                    info = $"{user.FirstName} {user.LastName}",
                    userid = user.SeqNum.ToString(),
                    email = user.Email,
                    token = jwtToken,
                    Encrpyt_token = AesOperation.EncryptString(jwtToken)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, info = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("signoutJWT")]
        public async Task<IActionResult> SignOutJWT()
        {
            try
            {
                // Retrieve cookies
                string uivCookie = Request.Cookies["Access_Token"];

                // Ensure the required cookies are present
                if (string.IsNullOrEmpty(uivCookie))
                {
                    return BadRequest(new { status = 400, redirect = "/", info = "Cookie not found" });
                }
                var main = JwtDecoder.DecodeJwt(uivCookie, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
                JObject userClaims = JObject.Parse(main);
                string nameId = (string)userClaims["nameid"];


                // Find the user in the database using the session (user ID)
                var user = await _context.UserInfos
                        .FirstOrDefaultAsync(u => u.SeqNum.ToString() == nameId);

                    if (user == null)
                    {
                        return BadRequest(new { status = false, info = "User not found" });
                    }

                    // Update the user's login status to "Logged Out" (assuming 0 means logged out)
                    user.IsLogin = false;
                    await _context.SaveChangesAsync();                

                // Expire the cookies by setting their expiration in the past
                var expiredCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddDays(-1), Secure = true, HttpOnly = false, SameSite = SameSiteMode.None };

                if (!string.IsNullOrEmpty(Request.Cookies["Access_Token"]))
                {
                    Response.Cookies.Append("Access_Token", "", expiredCookieOptions);
                }
                var responseSuccess = new
                {
                    status = 200,
                    redirect = "/", // Redirect to homepage or login page after sign out
                    info = "Successfully signed out"
                };

                return Ok(responseSuccess);
            }
            catch (Exception ex)
            {
                var expiredCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddDays(-1), Secure = false };

                Response.Cookies.Append("Access_Token", "", expiredCookieOptions);

                var responseError = new
                {
                    status = 500,
                    redirect = "/",
                    info = "An error occurred while signing out: " + ex.Message
                };

                return StatusCode(500, responseError);
            }
        }
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                // Retrieve cookies
                string uivCookie = Request.Cookies["UIV"];

                // Ensure the required cookies are present
                if (!string.IsNullOrEmpty(uivCookie))
                {
                    // Retrieve the session value (user ID in this case)
                    var userSession = HttpContext.Session.GetString("UIV");

                    if (string.IsNullOrEmpty(userSession))
                    {
                        return BadRequest(new { status = 400, redirect = "/", info = "Session not found" });
                    }

                    // Find the user in the database using the session (user ID)
                    var user = await _context.UserInfos
                        .FirstOrDefaultAsync(u => u.SeqNum.ToString() == userSession);

                    if (user == null)
                    {
                        return BadRequest(new { status = false, info = "User not found" });
                    }

                    // Update the user's login status to "Logged Out" (assuming 0 means logged out)
                    user.IsLogin = false;
                    await _context.SaveChangesAsync();
                }

                // Remove session data
                HttpContext.Session.Remove("UIV");
                HttpContext.Session.Remove("UEV");
                HttpContext.Session.Remove("UFLN");
                HttpContext.Session.Remove("URLE");

                // Expire the cookies by setting their expiration in the past
                var expiredCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddDays(-1), Secure = false };

                if (!string.IsNullOrEmpty(Request.Cookies["UEV"]))
                {
                    Response.Cookies.Append("UEV", "", expiredCookieOptions);
                }

                if (!string.IsNullOrEmpty(Request.Cookies["UIV"]))
                {
                    Response.Cookies.Append("UIV", "", expiredCookieOptions);
                }

                if (!string.IsNullOrEmpty(Request.Cookies["UFLN"]))
                {
                    Response.Cookies.Append("UFLN", "", expiredCookieOptions);
                }

                if (!string.IsNullOrEmpty(Request.Cookies["URLE"]))
                {
                    Response.Cookies.Append("URLE", "", expiredCookieOptions);
                }

                var responseSuccess = new
                {
                    status = 200,
                    redirect = "/", // Redirect to homepage or login page after sign out
                    info = "Successfully signed out"
                };

                return Ok(responseSuccess);
            }
            catch (Exception ex)
            {
                // If any exception occurs, ensure cookies and session are cleared
                HttpContext.Session.Remove("UIV");
                HttpContext.Session.Remove("UEV");
                HttpContext.Session.Remove("UFLN");
                HttpContext.Session.Remove("URLE");

                var expiredCookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddDays(-1), Secure = false };

                Response.Cookies.Append("UEV", "", expiredCookieOptions);
                Response.Cookies.Append("UIV", "", expiredCookieOptions);
                Response.Cookies.Append("UFLN", "", expiredCookieOptions);
                Response.Cookies.Append("URLE", "", expiredCookieOptions);

                var responseError = new
                {
                    status = 500,
                    redirect = "/",
                    info = "An error occurred while signing out: " + ex.Message
                };

                return StatusCode(500, responseError);
            }
        }

        //[HttpPost("token-verify")]
        //public async Task<IActionResult> TokenVerify([FromBody] Dictionary<string, string> data)
        //{            
        //    string userId = data["userID"]?.ToString();
        //    string userToken = data["userToken"]?.ToString();

        //    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userToken))
        //    {
        //        // Use LINQ to query user information based on userId and userToken
        //        var userRecord = await _context.UserInfos
        //            .Where(u => u.SeqNum.ToString() == userId && u.Token == userToken)
        //            .Select(u => new { u.Varified, u.SeqNum }) // Select only the necessary fields
        //            .FirstOrDefaultAsync();

        //        if (userRecord != null)
        //        {
        //            bool isVerified = (bool) userRecord.Varified;

        //            if (!isVerified)
        //            {
        //                // Update user verification status
        //                var userToUpdate = await _context.UserInfos.FirstOrDefaultAsync(u => u.SeqNum.ToString() == userId);
        //                if (userToUpdate != null)
        //                {
        //                    userToUpdate.Varified = true; // Assuming you want to set it to true
        //                    await _context.SaveChangesAsync();
        //                }

        //                return Ok(new { status = true, info ="Account is Verified" });
        //            }
        //            else
        //            {
        //                return BadRequest(new { status = false, info = "Account Already Exist" });
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new { status = false, info = "No Record Found" });
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest(new { status = false, info = "Emai" });
        //    }
        //}
        [HttpGet("token-verify")]
        public IActionResult TokenVerify()
        {
            var test = Request.Cookies["Access_Token"];
            if (string.IsNullOrEmpty(test))
            {
                return StatusCode(404, new { status = false, info = "Cookies are Empty" });
            }
            var main = JwtDecoder.DecodeJwt(test, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
            JObject userClaims = JObject.Parse(main);
            string nameId = (string)userClaims["nameid"];
            string email = (string)userClaims["email"];
            string Role = (string)userClaims["role"];

            return StatusCode(200, new { status = true, info = test, SEQ_NUM = nameId, Email = email, Rights = Role });
        }

        [Authorize]
        [HttpGet("fetchuserbyid")]
        public async Task<IActionResult> FetchUserById()
        {
            if (Request.Cookies["Access_Token"] != null)
            {
                string uivCookieValue = Request.Cookies["Access_Token"];
                if (string.IsNullOrEmpty(uivCookieValue))
                {
                    return StatusCode(404, new { status = false, info = "Cookies are Empty" });
                }
                var main = JwtDecoder.DecodeJwt(uivCookieValue, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
                JObject userClaims = JObject.Parse(main);
                string nameId = (string)userClaims["nameid"];
                string email = (string)userClaims["email"];
                string Role = (string)userClaims["role"];


                var userInfo = await _context.VwUserInfos
                    .Where(u => u.SeqNum.ToString() == nameId)
                    .Select(u => new
                    {
                        SEQ_NUM = nameId,
                        BALANCE = u.Balance,
                        USER_NAME = u.UserName,
                        Rights = u.Rights,
                        USER_TOTAL_IMEI = u.UserTotalImei,
                        USERS_EXPENDITURE = u.UsersExpenditure,
                        TOTAL_BALANCE = u.TotalBalance
                    })
                    .FirstOrDefaultAsync();

                if (userInfo == null)
                {
                    return NotFound(new { status = false, info = "No record found" });
                }

                var userBalance = await _context.ViewUserbalances
                    .Where(ub => ub.UserSeqNum.ToString() == nameId)
                    .Select(ub => new
                    {
                        BALANCE = ub.Balance,
                        PACKAGE_NAME = ub.PackageName
                    })
                    .ToListAsync();

                var userOrders = await _context.VwOrderProgresses
                    .Where(o => o.UserSeqNum.ToString() == nameId)
                    .Select(o => new
                    {
                        Completed = o.Completed,
                        Rejected = o.Rejected,
                        Pending = o.Pending,
                        Total = o.Total
                    })
                    .FirstOrDefaultAsync();

                var response = new
                {
                    status = true,
                    info = userInfo,
                    balance = userBalance,
                    orderinfo = userOrders
                };

                return Ok(response);

            }

            return Unauthorized(new { status = 401, redirect = "/" });
        }

        private void ExpireCookie(string cookieName)
        {
            if (Request.Cookies[cookieName] != null)
            {
                Response.Cookies.Append(cookieName, "", new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                    Secure = true, // Change to 'false' in development if needed
                    HttpOnly = true
                });
            }
        }
        private bool SendConfirmationEmail(string fullName, string email, string ip)
        {
            try
            {
                bool sendMail = false;
                string body = string.Empty;

                // Use IWebHostEnvironment to get the absolute path of the template file
                // var templatePath1 = Path.Combine(_webHostEnvironment.WebRootPath, "Utils", "LogInInfo.html");
                var templatePath = Path.GetFullPath("./Utils/LogInInfo.html");

                // Read the email template
                using (var streamReader = new StreamReader(templatePath))
                {
                    body = streamReader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(body))
                {
                    // Replace placeholders with actual values
                    body = body.Replace("@UserFullName", fullName);
                    body = body.Replace("@Ip", ip);
                }

                // Prepare email recipients
                List<string> toMails = new List<string> { email };

                // Send the email using EmailHelper (assumed to be an existing utility)
                sendMail = _emailHelper.SendMail("LogIn", body, toMails);

                // Return true if the email was sent successfully, false otherwise
                return sendMail;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    

}
