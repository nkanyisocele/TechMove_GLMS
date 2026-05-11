using GLMS.Web.Interfaces;
using GLMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GLMS.Web.Controllers;

public class ServiceRequestsController(
    IServiceRequestRepository requestRepo,
    IContractRepository contractRepo,
    IContractService contractService,
    ICurrencyService currencyService) : Controller
{
    // GET: ServiceRequests/Create
    public async Task<IActionResult> Create()
    {
        // Only show contracts so the user can pick one
        var contracts = await contractRepo.GetAllContractsAsync();
        ViewBag.ContractId = new SelectList(contracts, "ContractId", "ContractId");
        return View();
    }

    // POST: ServiceRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceRequest request)
    {
        // 1. Workflow Validation (Rubric 3)
        bool isActive = await contractService.IsContractActiveAsync(request.ContractId);

        if (!isActive)
        {
            ModelState.AddModelError("", "Cannot create request: The selected contract is NOT Active or has Expired.");
        }

        if (ModelState.IsValid)
        {
            // 2. Financial Integration (Rubric 5)
            // Convert USD input to ZAR using our External API Service
            request.CostZAR = await currencyService.ConvertUsdToZarAsync(request.CostUSD);

            await requestRepo.AddRequestAsync(request);
            await requestRepo.SaveAsync();
            return RedirectToAction("Index", "Contracts");
        }

        var contracts = await contractRepo.GetAllContractsAsync();
        ViewBag.ContractId = new SelectList(contracts, "ContractId", "ContractId");
        return View(request);
    }
}
