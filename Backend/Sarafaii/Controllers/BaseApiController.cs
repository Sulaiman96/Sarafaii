using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Sarafaii.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController(IMapper mapper) : ControllerBase
{
    public readonly IMapper Mapper = mapper;
}