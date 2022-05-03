using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace BabyMedsAzure.Controllers; 

[Controller]
[Route("api/[controller]")]
public class MedicineController: Controller {
    
    private readonly MongoDBService _mongoDBService;
    public MedicineController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Medicine>> Get() {
		return await _mongoDBService.GetAsync();
	}

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Medicine medicine) {
		await _mongoDBService.AddMedicineAsync(medicine);
    	return CreatedAtAction(nameof(Get), new { id = medicine.Id }, medicine);
	}


}