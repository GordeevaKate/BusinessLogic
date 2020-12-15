using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HospitalBusinessLogic.BindingModel;
using HospitalBusinessLogic.BusinessLogic;
using HospitalBusinessLogic.Enums;
using HospitalBusinessLogic.Interfaces;
using HospitalBusinessLogic.ViewModel;
using HospitalDatabaseImplement.Models;
using HospitalWebClient.Models;

namespace HospitalWebClient.Controllers
{
    public class VisitController : Controller
    {
        private readonly IVisitLogic _visitLogic;
        private readonly IDoctorLogic _doctorLogic;
        private readonly IPaymentLogic _paymentLogic;
        private readonly ReportLogic _reportLogic;
        public VisitController(IVisitLogic visitLogic, IDoctorLogic doctorLogic, IPaymentLogic paymentLogic, ReportLogic reportLogic)
        {
            _visitLogic = visitLogic;
            _doctorLogic = doctorLogic;
            _paymentLogic = paymentLogic;
            _reportLogic = reportLogic;
        }

        public IActionResult Visit()
        {
            ViewBag.Visits = _visitLogic.Read(new VisitBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Visit(ReportModel model)
        {
            var paymentList =new List<PaymentViewModel>();
            var visits = new List<VisitViewModel>();
            visits = _visitLogic.Read(new VisitBindingModel
            {
                ClientId = Program.Client.Id,
                DateFrom = model.From,
                DateTo = model.To
            });
            var payments = _paymentLogic.Read(null);
            foreach(var visit in visits)
            {
                foreach(var payment in payments)
                {
                    if (payment.ClientId == Program.Client.Id && payment.VisitId == visit.Id)
                        paymentList.Add(payment);
                }
            }
            ViewBag.Payments = paymentList;
            ViewBag.Visits = visits;
            string fileName = "E:\\data\\pdfreport.pdf";
            if (model.SendMail)
            {
                _reportLogic.SaveVisitPaymentsToPdfFile(fileName, new VisitBindingModel
                {
                    ClientId = Program.Client.Id,
                    DateFrom = model.From,
                    DateTo = model.To
                }, Program.Client.Email);
            }
            return View();
        }
        public IActionResult CreateVisit()
        {
            ViewBag.VisitDoctors = _doctorLogic.Read(null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateVisit(CreateVisitModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.VisitDoctors = _doctorLogic.Read(null);
                return View(model);
            }

            var visitDoctors = new List<VisitDoctorBindingModel>();

            foreach (var doctor in model.VisitDoctors)
            {
                if (doctor.Value > 0)
                {
                    visitDoctors.Add(new VisitDoctorBindingModel
                    {
                        DoctorId = doctor.Key,
                        Count = doctor.Value
                    });
                }
            }
            if (visitDoctors.Count == 0)
            {
                ViewBag.Products = _doctorLogic.Read(null);
                ModelState.AddModelError("", "Ни один doctor не выбран");
                return View(model);
            }
            _visitLogic.CreateOrUpdate(new VisitBindingModel
            {
                ClientId = Program.Client.Id,
                DateOfBuying = DateTime.Now,
                Status = VisitStatus.Принят,
                FinalCost = CalculateSum(visitDoctors),
                Duration = CalculateDuration(visitDoctors),
                VisitDoctors = visitDoctors
            });
            return RedirectToAction("Visit");
        }
        private int CalculateSum(List<VisitDoctorBindingModel> visitDoctors)
        {
            int sum = 0;

            foreach (var doctor in visitDoctors)
            {
                var doctorData = _doctorLogic.Read(new DoctorBindingModel { Id = doctor.DoctorId }).FirstOrDefault();

                if (doctorData != null)
                {
                    for (int i = 0; i < doctor.Count; i++)
                        sum += doctorData.Cost;
                }
            }
            return sum;
        }
        private int CalculateDuration(List<VisitDoctorBindingModel> visitDoctors)
        {
            int duration = 0;
            foreach (var doctor in visitDoctors)
            {
                var doctorData = _doctorLogic.Read(new DoctorBindingModel { Id = doctor.DoctorId }).FirstOrDefault();
                if (doctorData != null)
                {
                    for (int i = 0; i < doctor.Count; i++)
                        duration += doctorData.Duration;
                }
            }
            return duration;
        }
        public IActionResult Payment(int id)
        {
            var visit = _visitLogic.Read(new VisitBindingModel
            {
                Id = id
            }).FirstOrDefault();
            ViewBag.Visit = visit;
            ViewBag.LeftSum = CalculateLeftSum(visit);
            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            VisitViewModel visit = _visitLogic.Read(new VisitBindingModel
            {
                Id = model.VisitId
            }).FirstOrDefault();
            int leftSum = CalculateLeftSum(visit);
            if (!ModelState.IsValid)
            {
                ViewBag.Visit = visit;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            if (leftSum < model.Sum)
            {
                ViewBag.Visit = visit;
                ViewBag.LeftSum = leftSum;
                return View(model);
            }
            _paymentLogic.CreateOrUpdate(new PaymentBindingModel
            {
                VisitId = visit.Id,
                ClientId = Program.Client.Id,
                DatePayment = DateTime.Now,
                Sum = model.Sum
            });
            leftSum -= model.Sum;
            _visitLogic.CreateOrUpdate(new VisitBindingModel
            {
                Id = visit.Id,
                ClientId = visit.ClientId,
                DateOfBuying = visit.DateOfBuying,
                Duration = visit.Duration,
                Status = leftSum > 0 ? VisitStatus.Оплачен_не_полностью : VisitStatus.Оплачен,
                FinalCost = visit.FinalCost,
                VisitDoctors = visit.VisitDoctors.Select(rec => new VisitDoctorBindingModel
                {
                    Id = rec.Id,
                    VisitId = rec.VisitId,
                    DoctorId = rec.DoctorId,
                    Count = rec.Count
                }).ToList()
            });
            return RedirectToAction("Visit");
        }

        private int CalculateLeftSum(VisitViewModel visit)
        {
            int sum = visit.FinalCost;
            int paidSum = _paymentLogic.Read(new PaymentBindingModel
            {
                VisitId = visit.Id
            }).Select(rec => rec.Sum).Sum();

            return sum - paidSum;
        }
        public IActionResult SendWordReport(int id)
        {
            var visit = _visitLogic.Read(new VisitBindingModel { Id = id }).FirstOrDefault();
            string fileName = "E:\\data\\" + visit.Id + ".docx";
            _reportLogic.SaveVisitDoctorsToWordFile(fileName, visit, Program.Client.Email);
            return RedirectToAction("Visit");
        }
        public IActionResult SendExcelReport(int id)
        {
            var visit = _visitLogic.Read(new VisitBindingModel { Id = id }).FirstOrDefault();
            string fileName = "E:\\data\\" + visit.Id + ".xlsx";
            _reportLogic.SaveVisitDoctorsToExcelFile(fileName, visit, Program.Client.Email);
            return RedirectToAction("Visit");
        }
    }
}