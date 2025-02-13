using API.Model;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public ClientsController(ApplicationDbContext context)
		{
			this.context = context;
		}
		[HttpGet]
		public List<Client> GetClients()
		{
			return context.Clients.OrderByDescending(x => x.Id).ToList();
		}
		
		[HttpGet("{id}")]
		public IActionResult GetClient(int id)
		{
			var client = context.Clients.Find(id);
			if (client == null)
			{
				return NotFound();	
			}			

			return Ok(client);
		}

		[HttpPost]
		public IActionResult CreateClient(int id, ClientDto clientDto)
		{
			var otherclient = context.Clients.FirstOrDefault(c => c.Id != id && c.Email == clientDto.Email);
			if (otherclient != null)
			{
				ModelState.AddModelError("Email", "The email address is already used");
				var validation = new ValidationProblemDetails(ModelState);
				return BadRequest(validation);
			}
			var client = new Client
			{
				FirstName = clientDto.FirstName,
					LastName= clientDto.LastName,
					Email = clientDto.Email,
					Phone = clientDto.Phone ?? "",
					Address = clientDto.Address ?? "",
					Status = clientDto.Status,
					CreateDateAt = DateTime.Now
			};
			context.Clients.Add(client);	
			context.SaveChanges();	
			return Ok(client);

		}
		[HttpPut("{id}")]
		public IActionResult EditClient (int id, ClientDto clientDto) 
		{
			var otherclient = context.Clients.FirstOrDefault(c => c.Id != id && c.Email == clientDto.Email);
			if (otherclient != null)
			{
				ModelState.AddModelError("Email", "The email address is already used");
				var validation = new ValidationProblemDetails(ModelState);
				return BadRequest(validation);
			}

			var client = context.Clients.Find(id);
			if (client == null)
			{
				return NotFound();
			}
			client.FirstName=clientDto.FirstName;
			client.LastName=clientDto.LastName;
			client.Email=clientDto.Email;
			client.Phone = clientDto.Phone ?? "";
			client.Address = clientDto.Address ?? "";
			client.Status = clientDto.Status;

			context.SaveChanges();
			return Ok(client);


		}
		[HttpDelete("{id}")]
		public IActionResult DeleteClient (int id)
		{
			var client = context.Clients.Find(id);
			if (client == null)
			{
				return NotFound();
			}
			context.Clients.Remove(client);	
			context.SaveChanges();
			return Ok();


		}

	}
}
