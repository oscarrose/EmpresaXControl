
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DbLibrary.Models;
using DbLibrary.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace ControlCliente.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> _logger;

        private readonly EmpresaXContext _context;
        public ClientsController(ILogger<ClientsController> logger, EmpresaXContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        public async Task<IActionResult> Index(string SearchNameClient)
        {
            if (!String.IsNullOrEmpty(SearchNameClient))
            {
                var resultSearchClient = _context.Clients.Where(x => x.ClientName.Contains(SearchNameClient) && x.StatusClient==true);
                return View(await resultSearchClient.ToListAsync());
            }
            return View(await _context.Clients.Where(x=>x.StatusClient==true).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            Client client = new Client();
            client.ClientAddresses.Add(new ClientAddress());
            client.ClientAddresses.Add(new ClientAddress());

            return View(client);
        }
        //Method for add client with your addresses
        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
          
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var clientsMaster = new Client()
                        {
                            ClientName = client.ClientName,
                            LastName = client.LastName,
                            Identification = client.Identification,
                            Email = client.Email,
                            PhoneNumber = client.PhoneNumber,
                            ClientAddresses = client.ClientAddresses

                        };

                        _context.Clients.Add(clientsMaster);
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return BadRequest("Error inesperado");
                    }
                }
                return View(client);
            }

        }

        [HttpGet]
        //metohd for get the information cleint
        public IActionResult Details(int id)
        {
            Client client = _context.Clients.Include(x => x.ClientAddresses)
                .Where(y => y.ClientId == id).FirstOrDefault();
            return View(client);
        }

        [HttpGet]
        //metohd for get  the information a update the client
        public IActionResult Edit(int id)
        {
            Client client = _context.Clients.Include(x => x.ClientAddresses)
                .Where(y => y.ClientId == id).FirstOrDefault();
            return View(client);
        }

        [HttpPost]
        //metohd for update the client
        public IActionResult Edit(Client client)
        {
           List<ClientAddress> adrreses=_context.ClientAddresses.Where(x => x.ClientId == client.ClientId).ToList();    
            _context.ClientAddresses.RemoveRange(adrreses);
            _context.SaveChanges();

            _context.Update(client);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}