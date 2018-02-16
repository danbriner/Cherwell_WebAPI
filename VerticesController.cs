using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Triangle.Controllers
{
    public class VerticesController : ApiController
    {
        [Route("api/vertices/{V1x}/{V1y}/{V2x}/{V2y}/{V3x}/{V3y}")]
        public IHttpActionResult GetVertices(int V1x, int V1y, int V2x, int V2y, int V3x, int V3y)
        {
            try
            {
                ValidateVertex(V1x);
                ValidateVertex(V1y);
                ValidateVertex(V2x);
                ValidateVertex(V2y);
                ValidateVertex(V3x);
                ValidateVertex(V3y);

                return Ok(FindTriangle(V2x, V2y));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string FindTriangle(int V2x, int V2y)
        {
            try
            {
                // Since we are looking for a particular triangle (shape given in the document) then we only need to know a single point to determine which it is.
                // I picked the top left point because it was easier for my math.  :)
                int X = V2x / 10;
                int Y = V2y / 10;

                char Row = (char)((int)'A' + Y);
                int Column = X * 2 + 1;

                return Row.ToString() + Column.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateVertex(int Vertex)
        {
            try
            {
                if (Vertex < 0 || Vertex > 60)
                    throw new Exception("Invalid Point");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
