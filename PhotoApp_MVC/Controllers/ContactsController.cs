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

namespace PhotoApp_MVC.Controllers
{
    public class ContactsController : Controller
    {
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
        public async Task<IActionResult> Create([Bind("Name,EmailAdress,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("送信者名", contact.EmailAdress));
                    message.To.Add(new MailboxAddress("管理者名", "admin@example.com"));
                    message.Subject = "お問い合わせがありました";

                    message.Body = new TextPart("plain")
                    {
                        Text = $"お名前：{contact.Name}\nメールアドレス：{contact.EmailAdress}\nメッセージ：{contact.Message}"
                    };

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        await client.ConnectAsync("localhost", 1025, false);
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    return RedirectToAction(nameof(Index), "Home");
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
