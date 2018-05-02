using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using Web.ViewModels;
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
    }
}