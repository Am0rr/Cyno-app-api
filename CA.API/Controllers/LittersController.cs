using CA.BLL.DTOs;
using CA.BLL.Interfaces;
using CA.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LittersController(LitterService litterService) : ControllerBase
{
    private readonly ILitterService _litterService = litterService;

    [HttpPost("{litterId:guid}/publish")]
    public async Task<ActionResult<PublishResponse>> Publish(
        Guid litterId, [FromHeader(Name = "X-Breeder-id")] Guid breederId)
    {
        var result = await _litterService.PublishAsync(litterId, breederId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetLitters(
        [FromHeader(Name = "X-Breeder-id")] Guid breederId, [FromQuery] GetLittersRequest request)
    {
        var result = await _litterService.GetLittersAsync(breederId, request);

        return Ok(result);
    }
}