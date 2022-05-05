using System;
using Microsoft.AspNetCore.Mvc;
using BabyMedsAzure.Services;
using BabyMedsAzure.Models;

namespace BabyMedsAzure.Controllers;

	
[Route("api/[controller]")]
[ApiController]
public class MedicineController: Controller {
	
	private readonly MongoDBService _mongoDBService;
	public MedicineController(MongoDBService mongoDBService) {
		_mongoDBService = mongoDBService;
	}

	[HttpGet]
	public async Task<List<Medicine>> Get() {

		Console.WriteLine("Entered Get");

		_mongoDBService.AddMedicineTest("test1");
		_mongoDBService.AddMedicineTest("test2");
		return await _mongoDBService.GetAsync();
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] Medicine medicine) {
		await _mongoDBService.AddMedicineAsync(medicine);
		return CreatedAtAction(nameof(Get), new { id = medicine.Id }, medicine);
	}


}
