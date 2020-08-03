using System.Net.Mime;
using Exchange.Api.Properties;
using Exchange.Core.Contracts.Common;
using Exchange.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Api.Controllers.v1
{
    /// <summary>
    /// Diagnostics
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/diagnostics")]
    [Produces(MediaTypeNames.Application.Json)]
    public class DiagnosticsController : ControllerBase
    {
        private static readonly IApplicationInfo AppInfo = new ApplicationInfo();
        private static readonly VersionInfo VersionInfo = new VersionInfo();
        
        /// <summary>
        /// Show Application Version
        /// </summary>
        /// <returns></returns>
        [HttpGet("version")]
        [Produces(typeof(VersionInfo))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVersion()
        {
            return Ok(VersionInfo);
        }

        /// <summary>
        /// Show All Application Informational
        /// </summary>
        /// <returns></returns>
        [HttpGet("appinfo")]
        [Produces(typeof(ApplicationInfo))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetApplicationInfo()
        { 
            return Ok(AppInfo);
        }
    }
}