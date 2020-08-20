using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SafeAutoDriverUpload.Logic;

namespace SafeAutoDriverUpload.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UploadController : ControllerBase
  {
    private readonly ICustomerFileUploadService _fileUploadService;
    public UploadController(ICustomerFileUploadService fileUploadService)
    {
      _fileUploadService = fileUploadService;
    }

    [HttpPost]
    public ActionResult<List<DriverDto>> CustomerFile()
    {
      var file = Request.Form.Files[0];
      var dtos = _fileUploadService.UploadFile(file);
      dtos.Sort((x, y) => y.Miles.CompareTo(x.Miles));
      return Ok(dtos);
    }



  }
}
