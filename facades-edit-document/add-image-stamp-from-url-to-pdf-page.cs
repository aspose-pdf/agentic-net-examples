using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageUrl = "https://example.com/logo.png";

        // Download the image from the external URL into a memory stream
        using (HttpClient httpClient = new HttpClient())
        using (MemoryStream imageStream = new MemoryStream())
        {
            try
            {
                var response = httpClient.GetAsync(imageUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Warning: Unable to download image. HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
                    // Optional: load a local fallback image here instead of exiting.
                    return;
                }

                var imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                imageStream.Write(imageBytes, 0, imageBytes.Length);
                imageStream.Position = 0; // reset for reading
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading image: {ex.Message}");
                return;
            }

            // Initialize the PdfFileStamp facade using the non‑obsolete API
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Bind the source PDF
                fileStamp.BindPdf(inputPdf);

                // Create a stamp, bind the downloaded image, and configure its appearance
                Stamp stamp = new Stamp();
                stamp.BindImage(imageStream);          // use the image stream as stamp source
                stamp.SetOrigin(100f, 500f);           // position (X from left, Y from bottom)
                stamp.SetImageSize(120f, 60f);         // width and height of the stamp
                stamp.Opacity = 0.8f;                  // semi‑transparent
                stamp.IsBackground = false;            // place on top of page content
                stamp.Pages = new int[] { 2 };         // apply only to page 2 (1‑based indexing)

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Save the result to the output file
                fileStamp.Save(outputPdf);
            }
        }

        Console.WriteLine("Image stamp added to page 2 successfully.");
    }
}