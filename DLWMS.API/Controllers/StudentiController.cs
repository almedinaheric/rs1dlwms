using DLWMS.Core;
using DLWMS.Servis;
using Microsoft.AspNetCore.Mvc;

namespace DLWMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : Controller
    {
        public readonly IStudentService studentService;
        public StudentiController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return studentService.GetAll();
        }
    }
}
