using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using REACT_API.Models;
using REACT_API.OverLayModels;
using System.Data;
using System.Linq;

namespace REACT_API.DBCLASS
{
    public class DbClassHelper
    {
        public async Task<DataSet> Search4USERAsync(string nameId, string rights, HistoryRequest? request)
        {
            DataSet dsSearch = new DataSet();
            using (var dbContext = new MAIN01DbContext()) // Replace with your actual DbContext
            {
                IQueryable<VwUserImei> query = dbContext.VwUserImeis.AsQueryable();
                if (!string.IsNullOrEmpty(request.Imei))
                {
                    query = query.Where(imeiRecord => imeiRecord.Imei.Contains(request.Imei));
                }
                if (rights.ToLower().Contains("admin"))
                {
                    if (!string.IsNullOrEmpty(request.UserSeqNum))
                    {
                        query = query.Where(imeiRecord => imeiRecord.UserSeqNum.ToString() == request.UserSeqNum);
                    }
                }
                else
                {
                    query = query.Where(imeiRecord => imeiRecord.UserSeqNum.ToString() == nameId);
                }
                if (!string.IsNullOrEmpty(request.From))
                {
                    if (DateTime.TryParse(request.From, out DateTime fromDate))
                    {
                        query = query.Where(imeiRecord => imeiRecord.SearchDate >= fromDate);
                    }
                }
                if (!string.IsNullOrEmpty(request.To))
                {
                    if (DateTime.TryParse(request.To, out DateTime toDate))
                    {
                        query = query.Where(imeiRecord => imeiRecord.SearchDate <= toDate);
                    }
                }
                if (!string.IsNullOrEmpty(request.Service))
                {
                    query = query.Where(imeiRecord => imeiRecord.ServiceSeqNum.ToString() == request.Service);
                }
                long totalFiltered = await query.CountAsync();
                var paginatedData = await query.OrderByDescending(imeiRecord => imeiRecord.SeqNum)
                                                .Skip(request.Start)
                                                .Take(request.Length)
                                                .ToListAsync();
                DataTable dataTable = new DataTable("UserIMEIData");
                dataTable.Columns.Add("FULL_NAME");
                dataTable.Columns.Add("IMEI");
                dataTable.Columns.Add("IPhone Carrier");
                dataTable.Columns.Add("iPHONE CARRIER - FMI & BLACKLIST");
                dataTable.Columns.Add("iCLOUD ON/OFF");
                dataTable.Columns.Add("APPLE MDM STATUS");
                dataTable.Columns.Add("iPHONE SIM-LOCK");
                dataTable.Columns.Add("APPLE BASIC INFO");
                dataTable.Columns.Add("SERVICE_FEE");
                dataTable.Columns.Add("PACKAGE_NAME");
                dataTable.Columns.Add("SERVICE_NAME");
                dataTable.Columns.Add("ENTRY_DATE");
                dataTable.Columns.Add("Search_DATE");
                dataTable.Columns.Add("TOTAL_COST");
                dataTable.Columns.Add("APPLE ACTIVATION STATUS - IMEI/SN");
                dataTable.Columns.Add("MDM LOCK BYPASS - iPHONE/iPAD");
                dataTable.Columns.Add("WW BLACKLIST STATUS");
                dataTable.Columns.Add("USA T-MOBILE - CLEAN");
                dataTable.Columns.Add("USA AT&T - CLEAN/ACTIVE LINE");
                dataTable.Columns.Add("USA CRICKET - CLEAN & 6 MONTHS OLD");
                dataTable.Columns.Add("CLARO ALL COUNTRIES - PREMIUM (iPHONE 14)");
                dataTable.Columns.Add("CLARO ALL COUNTRIES - PREMIUM (iPHONE 15)");
                dataTable.Columns.Add("CLARO ALL COUNTRIES - PREMIUM (UP TO iPHONE 13)");
                dataTable.Columns.Add("SAMSUNG AT&T/CRICKET... ALL MODELS");
                dataTable.Columns.Add("ONEPLUS INFO");
                dataTable.Columns.Add("MOTOROLA INFO");
                dataTable.Columns.Add("GOOGLE PIXEL INFO");
                dataTable.Columns.Add("SAMSUNG INFO");
                dataTable.Columns.Add("SAMSUNG INFO - PRO");
                dataTable.Columns.Add("BRAND & MODEL INFO");

                foreach (var item in paginatedData)
                {
                    dataTable.Rows.Add(
                        item.FullName,                       // FULL_NAME
                        item.Imei,                           // IMEI
                        item.IphoneCarrier,                  // IPhone Carrier
                        item.IPhoneCarrierFmiBlacklist,      // iPHONE CARRIER - FMI & BLACKLIST
                        item.ICloudOnOff,                    // iCLOUD ON/OFF
                        item.AppleMdmStatus,                 // APPLE MDM STATUS
                        item.IPhoneSimLock,                  // iPHONE SIM-LOCK
                        item.AppleBasicInfo,                 // APPLE BASIC INFO
                        item.ServiceFee,                     // SERVICE_FEE
                        item.PackageName,                    // PACKAGE_NAME
                        item.ServiceName,                    // SERVICE_NAME
                        item.EntryDate,                      // ENTRY_DATE
                        item.SearchDate,                     // Search_DATE
                        item.TotalCost,                      // TOTAL_COST
                        item.AppleActivationStatusImeiSn,    // APPLE ACTIVATION STATUS - IMEI/SN
                        item.MdmLockBypassIPhoneIPad,        // MDM LOCK BYPASS - iPHONE/iPAD
                        item.WwBlacklistStatus,              // WW BLACKLIST STATUS
                        item.UsaTMobileClean,                // USA T-MOBILE - CLEAN
                        item.UsaAtTCleanActiveLine,          // USA AT&T - CLEAN/ACTIVE LINE
                        item.UsaCricketClean6MonthsOld,      // USA CRICKET - CLEAN & 6 MONTHS OLD
                        item.ClaroAllCountriesPremiumIPhone14,  // CLARO ALL COUNTRIES - PREMIUM (iPHONE 14)
                        item.ClaroAllCountriesPremiumIPhone15,  // CLARO ALL COUNTRIES - PREMIUM (iPHONE 15)
                        item.ClaroAllCountriesPremiumUpToIPhone13, // CLARO ALL COUNTRIES - PREMIUM (UP TO iPHONE 13)
                        item.SamsungAtTCricketAllModels,     // SAMSUNG AT&T/CRICKET... ALL MODELS
                        item.OneplusInfo,                    // ONEPLUS INFO
                        item.MotorolaInfo,                   // MOTOROLA INFO
                        item.GooglePixelInfo,                // GOOGLE PIXEL INFO
                        item.SamsungInfo,                    // SAMSUNG INFO
                        item.SamsungInfoPro,                 // SAMSUNG INFO - PRO
                        item.BrandModelInfo                  // BRAND & MODEL INFO
                    );
                }
                dsSearch.Tables.Add(dataTable);
                DataTable countTable = new DataTable("TotalCount");
                countTable.Columns.Add("TotalFiltered", typeof(long));
                countTable.Rows.Add(totalFiltered);
                dsSearch.Tables.Add(countTable);
            }
            return dsSearch;

        }

    }

}