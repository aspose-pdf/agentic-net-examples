using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageUrl = "https://example.com/logo.png";

        // Download the image into a memory stream – handle possible 404 or other HTTP errors gracefully
        byte[] imageBytes;
        using (HttpClient http = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = http.GetAsync(imageUrl).Result;
                response.EnsureSuccessStatusCode(); // throws if status is not 2xx
                imageBytes = response.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download image from '{imageUrl}'. Exception: {ex.Message}");
                // Optionally, you could provide a fallback image (e.g., a 1x1 transparent PNG) to keep the program running.
                // For this example we abort the operation because the stamp cannot be created without an image.
                return;
            }
        }

        // Use a MemoryStream so the Aspose stamp can read the image data
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Initialize the facade and bind the source PDF (new API)
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(inputPdf);

                // Create a stamp based on the downloaded image
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindImage(imageStream);          // bind the image stream
                stamp.SetOrigin(140, 400);             // position (x, y) from bottom‑left
                stamp.SetImageSize(50, 50);            // width and height of the stamp
                stamp.Opacity = 0.8f;                  // semi‑transparent
                stamp.IsBackground = false;            // place on top of page content
                stamp.Pages = new int[] { 2 };         // apply only to page 2 (1‑based)

                // Add the stamp and write the result (new API)
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPdf);
            }
        }
    }
}
