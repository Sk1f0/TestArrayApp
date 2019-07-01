using ArrayWepApi.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArrayWepApi.Controllers
{
    public class ValuesController : ApiController
    {
        private IArrayOperations _array;
        public ValuesController(IArrayOperations array)
        {
            _array = array;
        }

        // GET api/values
        public CustomArray GetArray()
        {
            return _array.Create();
        }

        [Route("api/values/rotate")]
        [HttpPost]
        public IHttpActionResult RotateArray(CustomArray array)
        {
            if (array?.Value == null)
                return BadRequest();

            return Ok(_array.Rotate(array));
        }

        [Route("api/values/import")]
        [HttpPost]
        public async Task<IHttpActionResult> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            StringBuilder sb = new StringBuilder();
            foreach (var file in provider.Contents)
            {
                byte[] fileArray = await file.ReadAsByteArrayAsync();
                sb.AppendLine(Encoding.ASCII.GetString(fileArray));
            }

            return Ok(_array.Import(sb.ToString()));
        }

        [Route("api/values/export")]
        [HttpPost]
        public IHttpActionResult Export(CustomArray array)
        {
            if (array?.Value == null)
                return BadRequest();

            return Ok(_array.Export(array));
        }

    }
}
