using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

namespace ExtractImagesUploadS3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Step 1: create a tiny sample image file (1x1 PNG)
            string imagePath = "sample.png";
            string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XG8cAAAAASUVORK5CYII=";
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            File.WriteAllBytes(imagePath, imageBytes);

            // Step 2: create a sample PDF that contains the image
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                Image pdfImage = new Image();
                pdfImage.File = imagePath;
                page.Paragraphs.Add(pdfImage);
                doc.Save("sample.pdf");
            }

            // Step 3: open the PDF and extract images, uploading each to S3 with SSE
            using (Document pdfDoc = new Document("sample.pdf"))
            {
                int imageIndex = 1;
                foreach (Page page in pdfDoc.Pages)
                {
                    foreach (XImage xImage in page.Resources.Images)
                    {
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            xImage.Save(imageStream);
                            imageStream.Position = 0;

                            using (HttpClient httpClient = new HttpClient())
                            {
                                ByteArrayContent content = new ByteArrayContent(imageStream.ToArray());
                                content.Headers.Add("x-amz-server-side-encryption", "AES256");
                                string s3Url = $"https://my-bucket.s3.amazonaws.com/image{imageIndex}.png";
                                HttpResponseMessage response = await httpClient.PutAsync(s3Url, content);
                                Console.WriteLine($"Uploaded image {imageIndex}: {(int)response.StatusCode}");
                            }
                        }
                        imageIndex++;
                        if (imageIndex > 4)
                        {
                            break; // evaluation mode limit: max 4 elements
                        }
                    }
                    if (imageIndex > 4)
                    {
                        break;
                    }
                }
            }
        }
    }
}
