using SkiServiceManagementApi.Models;
using SkiServiceManagementApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkiServiceManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly SkiServiceManagementService _skiServiceManagementService;

    public OrdersController(SkiServiceManagementService skiServiceManagementService) =>
        _skiServiceManagementService = skiServiceManagementService;

    [HttpGet]
    public async Task<List<Order>> Get() =>
        await _skiServiceManagementService.GetOrdersAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Order>> Get(string id)
    {
        var order = await _skiServiceManagementService.GetOrderAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Order newOrder)
    {
        await _skiServiceManagementService.CreateOrderAsync(newOrder);

        return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Order updatedOrder)
    {
        var order = await _skiServiceManagementService.GetOrderAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        updatedOrder.Id = order.Id;

        await _skiServiceManagementService.UpdateOrderAsync(id, updatedOrder);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var order = await _skiServiceManagementService.GetOrderAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        await _skiServiceManagementService.RemoveOrderAsync(id);

        return NoContent();
    }
}
