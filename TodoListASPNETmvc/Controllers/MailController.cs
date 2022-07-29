using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoListASPNETmvc.Models;
using TodoListASPNETmvc.Services;

namespace TodoListASPNETmvc.Controllers
{
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "Home" });
                return RedirectToRoute(routeValue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
