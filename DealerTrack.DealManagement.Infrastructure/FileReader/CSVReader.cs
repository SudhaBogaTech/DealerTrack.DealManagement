using DealerTrack.DealManagement.Application.Contracts.Infrastructure;
using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using DealerTrack.DealManagement.Infrastructure.FileReader;
using DealerTrack.DealManagement.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Infrastructure.FileReader
{
    [ExportMetadata("Name", "csv")]
    [Export("csv", typeof(IFileImporter))]
    public class CSVReader :  IFileImporter
    {
        
        public CSVReader()
        {

        }
        
        public async Task<List<CreateDealDTO>> ImportFile(IFormFile file)
        {
            var deals = new List<CreateDealDTO>();
            try
            {
                var enc1252 = CodePagesEncodingProvider.Instance.GetEncoding(1252);
                string errormessage = string.Empty;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    using (var sr = new StreamReader(memoryStream, enc1252))
                    {
                        bool isheaderRead = false;
                        string line;
                        string[] dealDataheader = new string[] { };
                        sr.BaseStream.Position = 0;
                        int linepos = 0;
                        do
                        {
                            line = sr.ReadLine();

                            if (line != null)
                            {


                                if (isheaderRead)
                                {
                                    linepos++;
                                    var dealData = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                                    if (!string.IsNullOrEmpty(errormessage))
                                        throw new Exception(errormessage);
                                    CreateDealDTO deal = AssignValues(dealData, dealDataheader, out errormessage);
                                    if (string.IsNullOrEmpty(errormessage))
                                        deals.Add(deal);

                                }
                                else
                                {
                                    dealDataheader = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                                    if (dealDataheader == null)
                                    {
                                        errormessage = "Header is required";


                                    }
                                    if (dealDataheader.Length > 0 && !dealDataheader.Any(m => m.ToLower() == "dealnumber"))
                                    {
                                        errormessage = "DealNumber column is a required field in the header";


                                    }
                                    isheaderRead = true;
                                    linepos = 1;
                                }


                            }
                        } while (line != null);
                    }
                }
                if (!string.IsNullOrEmpty(errormessage))
                {
                    
                    throw new Exception(errormessage);
                }
            }
            catch(Exception ex)
            {
               
                throw ;
            }
            //custom error handling here
          return deals;
            
        }

        private CreateDealDTO AssignValues(string[] dealData, string[] header, out string error)
        {
            var obj = new CreateDealDTO();
            int index = 0;
            var ci = new CultureInfo("en-US");
            error = string.Empty;
            string[] formats = new[] { "MM/dd/yyyy", "M/dd/yyyy", "MM-dd-yyyy" }.
                             Union(ci.DateTimeFormat.GetAllDateTimePatterns()).ToArray(); 
            foreach (string val in header)
            {
                Type type = obj.GetType();
               
                PropertyInfo prop = type.GetProperty(val);
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                //var dataType = propType.Name;
                if (index < dealData.Length)
                {
                    dealData[index] = dealData[index].TrimStart(' ', '"');
                    dealData[index] = dealData[index].TrimEnd('"');

                    switch (propType.Name)
                    {
                        case "Int32":
                            int dealNumber;
                            if (int.TryParse(dealData[index], out dealNumber))
                            {
                                prop.SetValue(obj, dealNumber);
                            }
                            else
                            {
                                error = string.Format("Error occured while reading dealNumber");
                                return null;
                            }
                            break;
                        case "Decimal":
                            Decimal price;
                            if (decimal.TryParse(dealData[index], out price))
                            {
                                prop.SetValue(obj, price);
                            }

                            break;
                        case "DateTime":
                            DateTime dateTime;
                            if (DateTime.TryParseExact(dealData[index], formats, CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out dateTime))
                            {
                                prop.SetValue(obj,
                                           dateTime);
                            }

                            break;
                        default:
                            if (!string.IsNullOrEmpty(dealData[index]))
                            {
                                prop.SetValue(obj, dealData[index]);
                            }
                            break;
                    }


                }
                index++;
            }
            return obj;
        }
    
    }
}
