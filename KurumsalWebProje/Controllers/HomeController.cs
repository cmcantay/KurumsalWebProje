using KurumsalWebProje.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using KurumsalWebProje.Models.Model;

namespace KurumsalWebProje.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Home
        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId); 
            return View();
        }
        public ActionResult SliderPartial() 
        {
            return View(db.Sliders.ToList().OrderByDescending(x=>x.SliderId));
        }
        public ActionResult HizmetPartial() 
        {
            return View(db.Hizmet.ToList());
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda() 
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }
        [Route("Hizmetlerimiz")]
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad=null,string email=null,string konu=null,string mesaj=null)
        {
            if (adsoyad!=null && email != null) 
            {
                try
                {
                    string smtpServer = "smtp.gmail.com"; //SMTP sunucusu adresi
                    int port = 587; //SMTP sunucusu port numarası
                    string senderEmail = "webproje4201@gmail.com"; //Gönderen e-posta adresi
                    string password = "znsyypwjbmqvzurb"; //Gönderen e-posta hesabının şifresi
                    string recipientEmail = "webproje4201@gmail.com"; //Alıcı e-posta adresi

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(recipientEmail);
                    mail.Subject =  konu; //E-posta konusu
                    mail.Body = email + mesaj; //E-posta içeriği

                    SmtpClient smtpClient = new SmtpClient(smtpServer, port);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mail);
                    ViewBag.Uyari = "Gönderildi.";

                    Console.WriteLine("E-posta gönderildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("E-posta gönderirken bir hata oluştu: " + ex.Message);
                }
                //try
                //{
                //    WebMail.SmtpServer = "smtp.gmail.com";
                //    WebMail.EnableSsl = false;
                //    WebMail.UserName = "webproje4201@gmail.com";
                //    WebMail.Password = "znsyypwjbmqvzurb";
                //    WebMail.SmtpPort = 587;
                //    WebMail.Send("webproje4201@gmail.com", konu, email + " " + mesaj);
                //    ViewBag.Uyari = "Gönderildi.";
                //}
                //catch
                //{
                //    ViewBag.Uyari = "Hata oluştu.";
                //}


            }
            return View();
        }
        [Route("BlogPost")]
        public ActionResult Blog(int Sayfa=1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Blog.Include("Kategori").OrderByDescending(x=>x.BlogId).ToPagedList(Sayfa,2));
        }
        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).Where(x => x.Kategori.KategoriId == id).ToPagedList(Sayfa, 2));
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b=db.Blog.Include("Kategori").Include("Yorums").Where(x=>x.BlogId == id).SingleOrDefault();
            return View(b);
        }
        public JsonResult YorumYap(string adsoyad,string eposta,string icerik,int blogid)
        {
            if (icerik==null) 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId = blogid, Onay = false });
            db.SaveChanges();
            return Json(false,JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }
        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x => x.BlogId));
        }
        public ActionResult PartialFooter()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            return PartialView();
        }
    }
}