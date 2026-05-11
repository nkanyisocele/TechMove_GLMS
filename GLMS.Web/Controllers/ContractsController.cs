using GLMS.Web.Interfaces;
using GLMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GLMS.Web.Controllers;

public class ContractsController(
    IContractRepository contractRepo,
    IClientRepository clientRepo,
    IFileService fileService) : Controller
{
    // 1. List all contracts (The "Hub")
    public async Task<IActionResult> Index()
    {
        var contracts = await contractRepo.GetAllContractsAsync();
        return View(contracts);
    }

    // 2. GET: Create Contract
    public async Task<IActionResult> Create()
    {
        // Load clients into a dropdown for the UI
        var clients = await clientRepo.GetAllClientsAsync();
        ViewBag.ClientId = new SelectList(clients, "ClientId", "Name");
        return View();
    }

    // 3. POST: Create Contract
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Contract contract, IFormFile pdfFile)
    {
        if (ModelState.IsValid)
        {
            // Handle File Upload (Rubric Item 4)
            if (pdfFile != null)
            {
                contract.SignedAgreementFileName = await fileService.UploadContractFileAsync(pdfFile);
            }

            await contractRepo.AddContractAsync(contract);
            await contractRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // If something failed, reload the clients dropdown
        var clients = await clientRepo.GetAllClientsAsync();
        ViewBag.ClientId = new SelectList(clients, "ClientId", "Name");
        return View(contract);
    }
}
