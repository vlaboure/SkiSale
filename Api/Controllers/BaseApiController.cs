using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    // contrôlleru utilisé par tous les autres contrôleurs
    // pour insérer les entêtes
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}