using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using BusinessLogic.BindingModel;

namespace WebClient.Controllers
{
    public class AgentController : Controller
    {
        private readonly IUserLogic _client;
        private readonly IAgentLogic _agent;
        public AgentController(IUserLogic client, IAgentLogic agent)
        {
            _client = client;
            _agent = agent;
        }
        public ActionResult Profile()
        {
            ViewBag.User = Program.User;
            ViewBag.Agent = Program.Agent;
            return View();
        }
    }
}