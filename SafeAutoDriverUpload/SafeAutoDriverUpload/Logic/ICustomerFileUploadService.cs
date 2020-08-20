using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SafeAutoDriverUpload.Logic
{
    public interface ICustomerFileUploadService
    {
    List<DriverDto> UploadFile(IFormFile file);
  }
}
