using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class UploadsController : ApiController
    {
        public IHttpActionResult GetUploads()
        {
            return Ok();
        }

        [Route("api/uploads/{FileName}")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage PostUploads(string fileName)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            if (Request.Content.IsMimeMultipartContent())
            {
                Request.Content.LoadIntoBufferAsync().Wait();
                Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        Image image = Image.FromStream(stream);
                        string filePath = HostingEnvironment.MapPath("~/Uploads");
                        if (filePath != null)
                        {
                            if (!Directory.Exists(filePath))
                            {
                                Directory.CreateDirectory(filePath);

                            }
                            string fullPath = Path.Combine(filePath, fileName + ".jpg");

                            double x = (300 / (double)image.Height);
                            var newWidth = image.Width * x;
                            var img = RezizeImage(image, newWidth, 300);
                            img.Save(fullPath);
                        }
                    }
                });
                return result;
            }
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
        }

        [Route("api/uploads/{FileName}")]
        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteImage(string fileName)
        {
            try
            {
                string imageFilePath = HostingEnvironment.MapPath("~/Uploads");
                if(imageFilePath == null) return new HttpResponseMessage(HttpStatusCode.NoContent);
                imageFilePath = Path.Combine(imageFilePath, fileName+".jpg");
                if (File.Exists(imageFilePath))
                {
                    File.Delete(imageFilePath);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }


        private Image RezizeImage(Image img, double maxWidth, double maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                double xRatio = img.Width / maxWidth;
                double yRatio = img.Height / maxHeight;
                double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }
    }
}