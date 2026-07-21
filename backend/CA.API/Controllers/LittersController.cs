using CA.BLL.DTOs;
using CA.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LittersController(ILitterService litterService,
    ICurrentBreederContext currentBreeder) : ControllerBase
{
    private readonly ILitterService _litterService = litterService;
    private readonly ICurrentBreederContext _currentBreeder = currentBreeder;

    [HttpPost("{litterId:guid}/publish")]
    public async Task<ActionResult<PublishResponse>> Publish(Guid litterId)
    {
        var result = await _litterService.PublishAsync(litterId, _currentBreeder.BreederId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedLitterResponse>> GetLitters([FromQuery] GetLittersRequest request)
    {
        var result = await _litterService.GetLittersAsync(_currentBreeder.BreederId, request);

        return Ok(result);
    }
}