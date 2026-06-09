using Microsoft.AspNetCore.Mvc;
using GLMS.Web.Services;
using GLMS.API.Models; // Reusing the same core models

namespace GLMS.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IContractService _contractService;

        // Inject the HTTP client service layer via constructor injection
        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return View(contracts); // Renders Index.cshtml view with data from the API
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,ClientName,Status,Value")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                var success = await _contractService.CreateContractAsync(contract);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Unable to create contract via the backend API API.");
            }
            return View(contract);
        }
    }
}
