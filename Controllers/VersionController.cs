using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Version = BookAPI.Models.Version;

namespace BookAPI.Controllers
{
    [Route("api/version")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            var versionInfo = new Version
            {
                Company = Assembly.GetEntryAssembly()
                                   .GetCustomAttribute<AssemblyCompanyAttribute>()
                                   .Company,
                Product = Assembly.GetEntryAssembly()
                                   .GetCustomAttribute<AssemblyProductAttribute>()
                                   .Product,
                ProductVersion = Assembly.GetEntryAssembly()
                                   .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                   .InformationalVersion
            };

            return Ok(versionInfo); 
        }
    }
}
