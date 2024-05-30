using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Repositories.IRepositories;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using PhotoApp_MVC.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace PhotoApp_MVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly GmailSettings _gmailSettings;
        public ContactsController(IOptions<GmailSettings> gmailSettings)
        {
            _gmailSettings = gmailSettings.Value;
        }


        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,EmailAddress,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("送信者名", _gmailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("管理者名", _gmailSettings.ToEmail));
                    message.Subject = "お問い合わせがありました";

                    message.Body = new TextPart("plain")
                    {
                        Text = $"お名前：{contact.Name}\nメールアドレス：{contact.EmailAddress}\nメッセージ：{contact.Message}"
                    };

                    //using (var client = new MailKit.Net.Smtp.SmtpClient())
                    //{
                    //    await client.ConnectAsync("localhost", 1025, false);
                    //    await client.SendAsync(message);
                    //    await client.DisconnectAsync(true);
                    //}

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync(_gmailSettings.FromEmail, _gmailSettings.Password);
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "メールの送信中にエラーが発生しました。");
                    return View(contact);
                }
            }

            return View(contact);
        }
    }
}
