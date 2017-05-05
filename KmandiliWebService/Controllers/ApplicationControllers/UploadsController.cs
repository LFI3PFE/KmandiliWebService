using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    //[RoutePrefix("api/Upload")]
    public class UploadsController : ApiController
    {
        public IHttpActionResult GetUploads()
        {
            return Ok("Yooo");
        }

        [Route("api/uploads/{FileName}")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage PostUploads(string FileName)
        {
            string path = "";
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
                        //var testName = content.Headers.ContentDisposition.FileName;
                        string filePath = HostingEnvironment.MapPath("~/Uploads");
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);

                        }
                        string fullPath = Path.Combine(filePath, FileName + ".jpg");
                        path = fullPath;

                        double x = (300 / (double)image.Height);
                        var newWidth = image.Width * x;
                        var img = RezizeImage(image, newWidth, 300);
                        //yourImage = resizeImage(yourImage, new Size(50,50));
                        img.Save(fullPath);
                    }
                });
                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        [Route("Upload/{FileName}")]
        [AllowAnonymous]
        [HttpDelete]
        public HttpResponseMessage DeleteImage(string FileName)
        {
            try
            {
                string imageFilePath = HostingEnvironment.MapPath("~/test");
                imageFilePath = Path.Combine(imageFilePath, FileName);
                //if (File.Exists(imageFilePath))
                //{
                //    File.Delete(imageFilePath);
                //    return new HttpResponseMessage(HttpStatusCode.OK);
                //}

                File.Delete(imageFilePath);
                return new HttpResponseMessage(HttpStatusCode.OK);
                //return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }


        private Image RezizeImage(Image img, double maxWidth, double maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                double xRatio = (double)img.Width / maxWidth;
                double yRatio = (double)img.Height / maxHeight;
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
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}