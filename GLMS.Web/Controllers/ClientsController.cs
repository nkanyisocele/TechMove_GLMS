
using GLMS.Web.Models;
using GLMS.API.Interfaces;
using GLMS.API.Models;

using Microsoft.AspNetCore.Mvc;

namespace GLMS.Web.Controllers;

public class ClientsController(IClientRepository clientRepo) : Controller
{
    // GET: Clients/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client client)
    {
        if (ModelState.IsValid)
        {
            await clientRepo.AddClientAsync(client);
            await clientRepo.SaveAsync();
            // Redirect to Contract creation once client is added
            return RedirectToAction("Create", "Contracts");
        }
        return View(client);
    }
}
