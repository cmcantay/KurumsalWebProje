using KurumsalWebProje.Models;
using KurumsalWebProje.Models.DataContext;
using KurumsalWebProje.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KurumsalWebProje.Helper;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;

namespace KurumsalWebProje.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Admin]
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {

            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count();
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.HizmetSay = db.Hizmet.Count();
            ViewBag.YorumSay = db.Blog.Count();


            var sorgu = db.Kategori.ToList();
            return View(sorgu);
        }
        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();
            if (login.Eposta == admin.Eposta && login.Sifre == admin.Sifre)
            {
                Session["adminId"] = login.AdminId;
                Session["adminEposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyari = "Yanlış";
            return View();
        }
        public ActionResult Logout()
        {
            Session["adminId"] = null;
            Session["adminEposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            var mail = db.Admin.Where(x => x.Eposta == eposta).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                //mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                mail.Sifre = Convert.ToString(yenisifre);
                db.SaveChanges();

                try
                {
                    string smtpServer = "smtp.gmail.com"; //SMTP sunucusu adresi
                    int port = 587; //SMTP sunucusu port numarası
                    string senderEmail = "webproje4201@gmail.com"; //Gönderen e-posta adresi
                    string password = "znsyypwjbmqvzurb"; //Gönderen e-posta hesabının şifresi
                    string recipientEmail = eposta; //Alıcı e-posta adresi

                    MailMessage email = new MailMessage();
                    email.From = new MailAddress(senderEmail);
                    email.To.Add(recipientEmail);
                    email.Subject = "Sifremi Unuttum"; //E-posta konusu
                    email.Body = "Epostanız: " + eposta + "Yeni Şifreniz: " + yenisifre; //E-posta içeriği

                    SmtpClient smtpClient = new SmtpClient(smtpServer, port);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(email);
                    ViewBag.Uyari = "Gönderildi.";
                }
                catch (Exception ex)
                {
                    ViewBag.Uyari = "Gönderilemedi!.";
                }




            }
            return View();
        }

        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin, string sifre)
        {
            //admin.Sifre = Crypto.Hash(sifre, "MD5");
            db.Admin.Add(admin);
            db.SaveChanges();
            return RedirectToAction("Adminler", "Admin");
        }
        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(int id, Admin admin, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
                //a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Sifre = admin.Sifre;
                a.Eposta = admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
            }

            return RedirectToAction("Adminler");
        }

        public ActionResult Delete(int id)
        {
            var delete = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            if (delete != null)
            {
                db.Admin.Remove(delete);
                db.SaveChanges();
            }
            return RedirectToAction("Adminler");
        }

    }
}