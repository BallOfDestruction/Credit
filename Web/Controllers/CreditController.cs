using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using Web.ViewModels;
using Web.ViewModels.Avialable;
using Web.ViewModels.Create;
using Web.ViewModels.GetList;
using Web.ViewModels.Pay;

namespace Web.Controllers
{
    public class CreditController : CommonController
    {
        public CreditController(Context context)
        {
            Context = context;
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var authorization = GetUserWithToken();
            if(authorization.Error != null) return Json(authorization.Error);

            var credits = Context.Credits.Where(w => w.UserId == authorization.User.Id).ToList();

            return Json(new GetListResponce(credits));
        }

        [HttpPost]
        public JsonResult Create([FromBody]CreateCreditRequest model)
        {
            var authorization = GetUserWithToken();
            if (authorization.Error != null) return Json(authorization.Error);

            var credit = new Credit();
            credit.Amount = model.Amount;
            credit.BankName = model.Bank;
            credit.DurationInMonth = model.Duration;
            credit.StartCredit = model.StartDate;
            credit.Name = model.Name;
            credit.TypeCredit = model.Type;
            credit.Procent = model.Procent;
            credit.InitPayment();
            credit.UserId = authorization.User.Id;
            credit.IsPay = false;

            Context.Add(credit);
            Context.SaveChanges();

            return Json(new CreateCreditResponce());
        }

        public JsonResult Pay([FromBody] PayCreditRequest model)
        {
            var authorization = GetUserWithToken();
            if (authorization.Error != null) return Json(authorization.Error);

            var credit = Context.Credits.FirstOrDefault(w => w.Id == model.CreditId);
            if (credit == null) return Json(new Error("notFound", "Такого кредита не найдено"));

            var payments = JsonConvert.DeserializeObject<List<PaymentModel>>(credit.ListPayment);
            var payment = payments.FirstOrDefault(w => w.Id == model.PaymentId);
            if (payment == null) return Json(new Error("notFound", "Такого платежа не найдено"));

            payment.IsPay = true;
            credit.ListPayment = JsonConvert.SerializeObject(payments);
            if (payments.All(w => w.IsPay))
                credit.IsPay = true;

            Context.SaveChanges();

            return Json(new PayCreditResponce {Credit = credit});
        }

        [HttpGet]
        public JsonResult GetAvialable()
        {
            var authorization = GetUserWithToken();
            if (authorization.Error != null) return Json(authorization.Error);

            var avialableCredits = Context.AvialableCredits?.ToList();

            return Json(new AvialableCreditResponce(avialableCredits));
        }

        public JsonResult UpdateAvialableNow()
        {
            UpdateAvialable();

            return Json("");
        }

        private void UpdateAvialable()
        {
            var link = "https://www.banki.ru/products/credits/search/";
            var webClient = new WebClient();

            Context.RemoveRange(Context.AvialableCredits.ToList());
            Context.SaveChanges();

            var avialable = new  List<AvialableCredit>();
            for (var page = 1; page < 100; page++)
            {
                string str = null;
 
                for (var i = 0; i < 5; i++)
                { 
                    try
                    {
                        str = webClient.DownloadString(link + "?page=" + page);
                        break;
                    }
                    catch (Exception e)
                    {
                        str = null;
                    }
                }

                if(string.IsNullOrEmpty(str))
                    break;

                try
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(str);

                    var procents = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[2]/div[1]/span[1]")?.InnerText?.Replace("\n","")?.Replace("от", "")?.Replace("до", "")?.Replace("\t", "")?.Replace("от","")?.Replace("%", "")?.Replace("&nbsp;","").Replace(",","."))
                        .Select(w => float.Parse(w,NumberStyles.AllowDecimalPoint))
                        .ToArray();

                    var bankName = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[1]/div[2]/div[2]")?.InnerText?.Replace("\t","")?.Replace("\n", ""))
                        .ToArray();

                    var amount = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[3]")?.InnerText?.Replace("\t", "")?.Replace("\n", "")?.Replace("от", "")?.Replace("до", "")?.Replace("%", "")?.Replace("&nbsp;", "")?.Replace(" ", ""))
                        .Select(float.Parse)
                        .ToArray();

                    var duration = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[4]")?.InnerText?.Replace("\t", "")?.Replace("\n", "")?.Replace("от", "")?.Replace("до", "")?.Replace("%", "")?.Replace("&nbsp;", "")?.Replace(" ", "")?.Replace(".", ""))
                        .Select(w => (w.Contains("лет") || w.Contains("года") ) ? int.Parse(w.Replace("лет", "").Replace("года", "")) * 12 : int.Parse(w.Replace("мес", "")))
                        .ToArray();

                    for (var c = 0; c < procents.Length; c++)
                    {
                        avialable.Add(new AvialableCredit(bankName[c], amount[c], procents[c], duration[c]));
                    }

                }
                catch (Exception e)
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(str);

                    var procents = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[2]/div[1]/span[1]")?.InnerText?.Replace("\n", "")?.Replace("\t", "")?.Replace("от", "")?.Replace("%", "")?.Replace("&nbsp;", "").Replace(",", "."))
                        .ToArray();

                    var amount = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[3]")?.InnerText?.Replace("\t", "")?.Replace("\n", "")?.Replace("до", "")?.Replace("%", "")?.Replace("&nbsp;", "")?.Replace(" ", ""))
                        .ToArray();

                    var duration = doc.DocumentNode
                        .SelectNodes("/html[1]/body[1]/div[3]/div[3]/div[2]/section[1]/div[1]/div[1]/div[2]").Elements().Where(w => w.Name == "div")
                        .Select(w => w.SelectSingleNode("div[1]/div[4]")?.InnerText?.Replace("\t", "")?.Replace("\n", "")?.Replace("до", "")?.Replace("%", "")?.Replace("&nbsp;", "")?.Replace(" ", "")?.Replace(".", ""))
                        .ToArray();
                }
            }

            Context.AddRange(avialable);
            Context.SaveChanges();
        }
    }
}