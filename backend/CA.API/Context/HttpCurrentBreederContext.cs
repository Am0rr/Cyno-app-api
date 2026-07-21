using CA.BLL.Interfaces;
using CA.DAL.Exceptions;

namespace CA.API.Context;

public class HttpCurrentBreederContext : ICurrentBreederContext
{
    private const string HeaderName = "X-Breeder-Id";
    public readonly IHttpContextAccessor _httpContextAccessor;
    public HttpCurrentBreederContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid BreederId
    {
        get
        {
            var header = _httpContextAccessor.HttpContext?.Request.Headers[HeaderName].FirstOrDefault();

            if (string.IsNullOrEmpty(header) || !Guid.TryParse(header, out var breederId))
                throw new DomainException($"Missing or invalid {HeaderName} header.");

            return breederId;
        }
    }
}