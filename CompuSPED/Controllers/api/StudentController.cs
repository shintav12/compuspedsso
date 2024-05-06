using CompuSPED.Controllers.api.Base;
using CompuSPED.Service.Lumen;
using System.Threading.Tasks;
using System.Web.Http;

namespace CompuSPED.Controllers.api
{
    [RoutePrefix("api/student")]
    public class StudentController : BaseController
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("merge")]
        public async Task<IHttpActionResult> MergeStudents()
        {
            var response = await _studentService.GetStudents();

            return Ok(response.Result);
        }
    }
}
