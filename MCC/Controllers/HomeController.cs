using System;
using System.Web;
using System.Linq;
using MCC.Domain;
using System.Web.Mvc;
using System.Web.Routing;
using ExportToExcel;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           return RedirectToAction("FeaturedHotels", new RouteValueDictionary(new { controller = "Home", action = "FeaturedHotels", Audience = "jetblue" }));
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProvideFeedback()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult ProvideFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                FeedbackRepository.SaveFeedback(feedback, Request.Files.Count > 0 ? Request.Files[0] : null);
                ViewBag.Title = TranslationRepository.GetLabel("Layout", "ProvideFeedback") + " | " + Audience.Current.ToString().ToUpper();
                ViewBag.BackgroundImageClass = "provide-feedback";
                return View("ThankYouMessage", (object)"ProvideFeedback");
            }
            return View(feedback);
        }

        public ActionResult Report(string id)
        {            
            return View((object)id);
        }

        public ActionResult FeedbackDetails(int? id)
        {
            ViewBag.ImgRootURL = System.Configuration.ConfigurationManager.AppSettings["ImgRootURL"];
            if (id.HasValue)
            {
                var feedback = FeedbackRepository.GetFeedbackById(id.Value);
                if(feedback== null)
                    return View("PageNotFound");
                return View(feedback);
            }

            return View();
        }

        public ActionResult AddFeedbackComment(Guid FeedbackId, int FeedbackNumber, int FeedbackStatus, string ResponseComment, bool SendNotification)
        {
           // var userId = Guid.Parse(HttpContext.User.Identity.Name);
            var userId = new Guid("6892C499-32F5-4A03-AE2D-3BD5820EC4A8");
 
            var comment = new FeedbackComment
            {
                FeedbackId = FeedbackId,
                FeedbackStatus = (FeedbackStatus)FeedbackStatus,
                ResponseComment = ResponseComment,
                UserName = UserRepository.GetUserById(userId).UserName
            };
            FeedbackCommentRepository.AddFeedbackComment(comment);
            if (SendNotification)
            {
                var feedback = FeedbackRepository.GetFeedbackById(FeedbackNumber);
                var feedbackUser = UserRepository.GetUserById(feedback.UserId);

                Email.Email.SendEmail(feedbackUser.Email, Email.EmailTemplates.FeedbackResponse, feedback);
            }
            return RedirectToAction("FeedbackDetails", new {id = FeedbackNumber } );
        }

        public ActionResult AssociateNewHotel(Guid FeedbackId, int FeedbackNumber, Guid NewHotel, string Comments, int SendNotification)
        {
            var userId = new Guid("6892C499-32F5-4A03-AE2D-3BD5820EC4A8");
            FeedbackRepository.AssociateNewHotel(FeedbackId, NewHotel, Comments, userId);
            return RedirectToAction("FeedbackDetails", new {id= FeedbackNumber } );
        }
        public ActionResult ChangeFeedbackTypeTopicPriority(Guid FeedbackId, int FeedbackNumber, int Priority, Guid FeedbackTopic, int FeedbackType, bool SendNotification)
        {
            var userId = new Guid("6892C499-32F5-4A03-AE2D-3BD5820EC4A8");
            FeedbackRepository.ChangeFeedbackTypeTopicPriority(FeedbackId, FeedbackTopic, Priority, FeedbackType, userId);
            return RedirectToAction("FeedbackDetails", new {id= FeedbackNumber } );
        }
        public ActionResult MyTravel()
        {
            ViewBag.Title = TranslationRepository.GetLabel("Layout", "Dashboard") + " | " + Audience.Current.ToString().ToUpper();
            ViewBag.BackgroundImageClass = "my-travel";
            return View();
        }
        public ActionResult FeaturedHotels(string id)
        {
            var airline = Audience.Current.Airline;
            if(airline==null)
            {
                //redirect to error page
            }
            var model = new Models.FeaturedHotelsModel();
            model.Airports = AirportRepository.GetAirports(airline.FeaturedAirportIds).OrderBy(x => x.City).ToList();
            if(string.IsNullOrEmpty(id))
                model.Hotels = FeaturedHotelRepository.GetFeaturedHotels(airline.FeaturedHotelIds).OrderBy(x => x.City).ToList();
            else
                model.Hotels = FeaturedHotelRepository.GetFeaturedHotels(airline.Id, id).OrderBy(x => x.City).ToList();

            ViewBag.Title = TranslationRepository.GetLabel("Layout", "Dashboard") + " | " + Audience.Current.ToString().ToUpper();
            ViewBag.ImgRootURL = System.Configuration.ConfigurationManager.AppSettings["ImgRootURL"];
            ViewBag.BackgroundImageClass = "featured-hotels";

            return View(model);
        }

        public ActionResult Hotel(int id)
        {
            //var dataMidified = DateTime.Parse("01/01/2016");
            //var translationModified = TranslationRepository.GetLastModifiedDate("_Page_Views_home_Hotel_cshtml");
            //if (!this.IsModified(translationModified.Value) && !this.IsModified(dataMidified))
            //    return this.NotModified();
            //var lastModified = dataMidified > translationModified ? dataMidified : translationModified;
            //Response.AddHeader("Last-Modified", lastModified.Value.ToUniversalTime().ToString("R"));
            ViewBag.ImgRootURL = System.Configuration.ConfigurationManager.AppSettings["ImgRootURL"];
            var model = new Models.HotelInfoModel(id);
            return View(model);
        }

        public ActionResult Dashboard()
        {
            ViewBag.Title = TranslationRepository.GetLabel("Layout", "FeedbackList") + " | " + Audience.Current.ToString().ToUpper();
            ViewBag.BackgroundImageClass = "hotel-reservation";
            var userId = new Guid("6892C499-32F5-4A03-AE2D-3BD5820EC4A8");
            var model=FeedbackDashboardItemRepository.GetUserFeedbacks(userId);

            return View(model);
        }

        //public ActionResult EmailTemplate()
        //{
        //    return View();
        //}
        public ActionResult EmailTemplate(int? id, string language)
        {
            var model = new EmailTemplate { Language = language??"EN", Template = "" };
            if(id.HasValue)
                model = EmailTemplateRepository.GetTemplate(id.Value, language);
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EmailTemplate(int id, string Email, string Language, string Subject)
        {
            EmailTemplateRepository.Save(id, Language, Email, Subject);
            var model = EmailTemplateRepository.GetTemplate(id, Language);
            return View(model);
        }

        public ActionResult PreviewEmail(int id, string language, string itemid)
        {            
            var tmpl = EmailTemplateRepository.GetTemplate(id, language);
            var obj = tmpl.GetObjectById(itemid);
            var body = Email.EmailFormatter.Format(tmpl.Template, obj);
            return Content("<html><body>"+ body + "</body></html>", "text/html");
        }

        public ActionResult CityAndHotels()
        {
            var airlineId = Guid.Empty;
            var airline = Audience.Current.Airline;
            if (airline != null)
                airlineId = Audience.Current.Airline.Id;
            ViewBag.Title = TranslationRepository.GetLabel("Layout", "CityAndHotels") + " | " + Audience.Current.ToString().ToUpper();
            ViewBag.BackgroundImageClass = "hotel-reservation";
            var model=HotelRepository.GetCityAndHotels(airlineId);
            return View(model);
        }

        public void ExportFeedbacksToExcel()
        {
            var userId = new Guid("6892C499-32F5-4A03-AE2D-3BD5820EC4A8");
            var dt=FeedbackDashboardItemRepository.GetFeedbacks(userId);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=Report.xlsx");
            var ms = new MemoryStream();
            CreateExcelFile.CreateExcelDocument(dt, ms);
            Response.BinaryWrite(ms.ToArray());
            ms.Close();
            ms.Dispose();
            Response.End();
        }

        public string GetUserList(string searchStr)
        {
            var airline = Audience.Current.Airline;
            if (airline == null)
                return "";

            var l=UserRepository.GetAirlineUsers(airline.Id, searchStr);
            return l.AsJson();
        }

        [HttpPost]
        public ActionResult SearchUsers(string UserName, string Email, string UserStatus, Guid? Airline)
        {
            var userId = new Guid("5734B3DE-A11D-4312-8870-B4FFA230FE8F");//jbladmin
            ViewBag.Title = TranslationRepository.GetLabel("Layout", "FeedbackList") + " | " + Audience.Current.ToString().ToUpper();
            ViewBag.BackgroundImageClass = "hotel-reservation";
           // Guid? airline = string.IsNullOrEmpty(Airline) ? (Guid?)null : Guid.Parse(Airline);
            var model = new Models.SearchUsersModel {Airline = Airline, Email = Email, UserName = UserName, UserStatus = 0 }; //.GetUsers(userId, language, UserName, Email,-1, airline);
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchUsers()
        {
            ViewBag.Title = TranslationRepository.GetLabel("Layout", "FeedbackList") + " | " + Audience.Current.ToString().ToUpper();
            ViewBag.BackgroundImageClass = "hotel-reservation";
            var model = new Models.SearchUsersModel();
            return View(model);
        }
    }
}