using Bank_System.Interfaces;
using Bank_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly Bank_SystemContext _context;
        private readonly IPassportRepository _passportRepository;

        public PassportController(Bank_SystemContext context, IPassportRepository passportRepository)
        {
            _context = context;
            _passportRepository = passportRepository;
        }

        [HttpPost("UploadImage/{username}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadImage(string username, List<IFormFile> fileUpload)
        {
            if (fileUpload.Count < 0)
            {
                return StatusCode(500, "There are problems during upload");
            }

            int n = 0;
            string[] images = new string[2];

            foreach (var file in fileUpload)
            {

                string path = _passportRepository.GetFilePath(username);

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var imagePath = path + "\\" + file.FileName;

                if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
                using (FileStream fileStream = System.IO.File.Create(imagePath))
                {
                    await file.CopyToAsync(fileStream);
                    images[n++] = imagePath;
                }
            }

            _passportRepository.CreatePassport(username, images);
            return Ok("Upload Done!");
        }
    }
}
