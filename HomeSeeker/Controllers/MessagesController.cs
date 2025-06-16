using Core.Entities;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeSeeker.Controllers
{
    public class MessagesController : Controller
    {
        private readonly HomeSeekerDbContext _context;
        private readonly UserManager<User> _userManager;

        public MessagesController(HomeSeekerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Просмотр диалога
        public async Task<IActionResult> Chat(int receiverId)
        {
            var currentUserId = (await _context.Profiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId.ToString() == _userManager.GetUserId(User)))?.Id;

            if (currentUserId == null)
                return Unauthorized();

            var messages = await _context.Messages
                .Where(m =>
                    (m.SenderId == currentUserId && m.ReceiverId == receiverId) ||
                    (m.SenderId == receiverId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            ViewBag.ReceiverId = receiverId;
            ViewBag.CurrentUserId = currentUserId;

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int receiverId, string content)
        {
            var currentUserId = (await _context.Profiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id.ToString() == _userManager.GetUserId(User)))?.Id;

            if (currentUserId == null)
                return Unauthorized();

            var message = new Message
            {
                SenderId = currentUserId.Value,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { receiverId });
        }
    }
}
