using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Triangle.Controllers
{
    public class TriangleController : ApiController
    {
        [Route("api/triangle/{id}")]
        public IHttpActionResult GetTriangle(string id)
        {
            char Row;
            int Column;

            try
            {
                if (!char.TryParse(id.Substring(0, 1), out Row))
                {
                    throw new Exception("Invalid Row Value");
                }

                if (!int.TryParse(id.Substring(1), out Column))
                {
                    throw new Exception("Invalid Column Value");
                }

                return Ok(FindVertices(Row, Column));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string FindVertices(char Row, int Column)
        {
            string result = "";

            try
            {
                ValidateRow(Row);
                ValidateColumn(Column);

                // If we can determine which even/odd pair of triangles we are working with, we already know all of the points on the square and therefore the two points on the hypotenuse.
                // Since there are 6 "squares", we can use the column number to determine the "index".
                int lowX = 10 * ((Column - 1) / 2);
                int highX = 10 * ((Column + 1) / 2);

                // Determine the "index" of the row based on A being the first and at the origin (top left since we're dealing with pixels).
                int RowInt = (int)Row - (int)'A';

                // From the index we multiply by 10 to get the pixels.
                int lowY = 10 * RowInt;
                int highY = 10 * (RowInt + 1);

                // Now we need to determine which is the 3rd point and for that we just need to know if it's the even or odd triangle.
                if (Column % 2 == 1)
                {
                    result = "{" + lowX.ToString() + "," + lowY.ToString() + "},{" + highX.ToString() + "," + highY.ToString() + "},{" + lowX.ToString() + "," + highY.ToString() + "}";
                }
                else
                {
                    result = "{" + lowX.ToString() + "," + lowY.ToString() + "},{" + highX.ToString() + "," + highY.ToString() + "},{" + highX.ToString() + "," + lowY.ToString() + "}";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateRow(char Row)
        {
            try
            {
                if (Row < 'A' || Row > 'F')
                {
                    throw new Exception("Row out of bounds");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateColumn(int Column)
        {
            try
            {
                if (Column < 1 || Column > 12)
                {
                    throw new Exception("Column out of bounds");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
