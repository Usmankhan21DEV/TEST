using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using REACT_API.CommonClasses;
using REACT_API.Data;
using REACT_API.DBCLASS;
using REACT_API.Models;
using REACT_API.OverLayModels;
using REACT_API.Utils;
using System.Data;

namespace REACT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrmController : ControllerBase
    {
        private readonly MAIN01DbContext _context;

        public FrmController(MAIN01DbContext context)
        {
            _context = context;
        }

        [HttpPost("history")]
        public async Task<IActionResult> GetHistory([FromBody] HistoryRequest request)
        {
            try
            {
                DbClassHelper dbClassHelper = new DbClassHelper();
                string uivCookieValue = Request.Cookies["Access_Token"];
                var main = JwtDecoder.DecodeJwt(uivCookieValue, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
                JObject userClaims = JObject.Parse(main);
                string nameId = (string)userClaims["nameid"];
                string Role = (string)userClaims["role"];
                // Fetch IMEI data from the database
                DataSet dsIMEI = await dbClassHelper.Search4USERAsync(nameId, Role, request);
                List<List<object>> imeiList = new List<List<object>>();
                foreach (DataRow dr in dsIMEI.Tables[0].Rows)
                {
                    var rowIMEI = new List<object>
                    {
                        dr["FULL_NAME"].ToString(),
                        dr["IMEI"].ToString(),
                        dr["IPhone Carrier"].ToString(),
                        dr["iPHONE CARRIER - FMI & BLACKLIST"].ToString(),
                        dr["iCLOUD ON/OFF"].ToString(),
                        dr["APPLE MDM STATUS"].ToString(),
                        dr["iPHONE SIM-LOCK"].ToString(),
                        dr["APPLE BASIC INFO"].ToString(),
                        dr["SERVICE_FEE"].ToString(),
                        dr["PACKAGE_NAME"].ToString(),
                        dr["SERVICE_NAME"].ToString(),
                        dr["ENTRY_DATE"].ToString(),
                        dr["Search_DATE"].ToString(),
                        dr["TOTAL_COST"].ToString(),
                        dr["APPLE ACTIVATION STATUS - IMEI/SN"].ToString(),
                        dr["MDM LOCK BYPASS - iPHONE/iPAD"].ToString(),
                        dr["WW BLACKLIST STATUS"].ToString(),
                        dr["USA T-MOBILE - CLEAN"].ToString(),
                        dr["USA AT&T - CLEAN/ACTIVE LINE"].ToString(),
                        dr["USA CRICKET - CLEAN & 6 MONTHS OLD"].ToString(),
                        dr["CLARO ALL COUNTRIES - PREMIUM (iPHONE 14)"].ToString(),
                        dr["CLARO ALL COUNTRIES - PREMIUM (iPHONE 15)"].ToString(),
                        dr["CLARO ALL COUNTRIES - PREMIUM (UP TO iPHONE 13)"].ToString(),
                        dr["SAMSUNG AT&T/CRICKET... ALL MODELS"].ToString(),
                        dr["ONEPLUS INFO"].ToString(),
                        dr["MOTOROLA INFO"].ToString(),
                        dr["GOOGLE PIXEL INFO"].ToString(),
                        dr["SAMSUNG INFO"].ToString(),
                        dr["SAMSUNG INFO - PRO"].ToString(),
                        dr["BRAND & MODEL INFO"].ToString()
                    };
                    imeiList.Add(rowIMEI);
                }

                var responseSuccess = new
                {
                    status = true,
                    info = imeiList,
                    totalRecords = dsIMEI.Tables["TotalCount"].Rows[0]["TotalFiltered"],
                    totalFiltered = dsIMEI.Tables["TotalCount"].Rows[0]["TotalFiltered"]
                };

                return Ok(responseSuccess);
            }
            catch (Exception ex)
            {
                var responseFailure = new
                {
                    status = false,
                    info = ex.Message,
                };
                return StatusCode(500, responseFailure);
            }
        }

        [Authorize]
        [HttpGet("fetch-services")]
        public async Task<IActionResult> FetchServices()
        {
            string uivCookieValue = Request.Cookies["Access_Token"];
            var main = JwtDecoder.DecodeJwt(uivCookieValue, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
            JObject userClaims = JObject.Parse(main);
            string nameId = (string)userClaims["nameid"];
            //  string Role = (string)userClaims["role"]

            var serviceList = await _context.VwUserServiceCosts
                .Where(s => s.UserSeqNum.ToString() == nameId && s.Balance > s.Cost)  // Filter by USER_SEQ_NUM and Balance > Cost
                .OrderByDescending(s => s.BalanceCreatedDate)                  // Order by BALANCE_CREATED_DATE descending
                .Take(22)                                                        // Limit the result to 22 rows
                .Select(s => new
                {
                    s.ServiceSeqNum,
                    s.CompleteServiceName
                })
                .ToListAsync();

            if (serviceList.Any())
            {
                var response = new
                {
                    status = true,
                    info = serviceList
                };
                return Ok(response);
            }
            else
            {
                var response = new
                {
                    status = false,
                    info = "No records found."
                };
                return Ok(response);
            }
        }
        [Authorize]
        [HttpGet("fetch-users")]
        public async Task<IActionResult> FetchUsers()
        {
            string uivCookieValue = Request.Cookies["Access_Token"];
            var main = JwtDecoder.DecodeJwt(uivCookieValue, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
            JObject userClaims = JObject.Parse(main);
            string nameId = (string)userClaims["nameid"];
            string Role = (string)userClaims["role"];

            if (Role.Contains("admin", StringComparison.CurrentCultureIgnoreCase))
            {
                var userList = await _context.VwUserInfos
                .Select(s => new
                {
                    s.SeqNum,
                    s.UserName
                })
                .ToListAsync();

                if (userList.Any())
                {
                    var response = new
                    {
                        status = true,
                        info = userList
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = false,
                        info = "No records found."
                    };
                    return Ok(response);
                }
            }
            else
            {
                var response = new
                {
                    status = false,
                    info = "Get Out"
                };
                return Ok(response);
            }
        }

        [HttpPost("check_imei_info")]
        public async Task<IActionResult> ProcessBulkImeiCheck([FromBody] ImeiRequest request)
        {
            FrmInfo _frminfo= new FrmInfo();
            string uivCookieValue = Request.Cookies["Access_Token"];
            var main = JwtDecoder.DecodeJwt(uivCookieValue, "YourVeryLongSecureSecretKeyHere1234567890!@#$");
            JObject userClaims = JObject.Parse(main);
            string nameId = (string)userClaims["nameid"];
            string email = (string)userClaims["email"];
            string role = (string)userClaims["role"];

            if (request.VMOpt.Contains("email"))
            {
               
                    BackgroundJob.Enqueue(() => _frminfo.ProcessBulkCheck(new BulkCheckRequest
                    {
                        Imei = request.Imei,
                        Rights = role,
                        Id = nameId,
                        Email = email,
                        Service = request.Service
                    }));


                RecurringJob.AddOrUpdate("CheckOrderStatusJob", () => _frminfo.ProcessBulkCheck(new BulkCheckRequest
                {
                    Imei = request.Imei,
                    Rights = role,
                    Id = nameId,
                    Email = email,
                    Service = request.Service
                }), "*/1 * * * *"); // This CRON expression schedules the job to run every 5 minutes

                var response = new
                {
                    status = false,
                    info = "Bulk processing has started. You will receive an email when it is complete, and payment will be processed promptly."
                };

                return Ok(response);
            }
            else
            {
                // Return the result immediately if the list is small
                return Ok("Yo wai mo");
            }
        }

       

    }

}
