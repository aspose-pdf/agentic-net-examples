using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageUrl  = "https://example.com/logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Download the image from the external URL into a memory stream
        byte[] imageBytes;
        using (var httpClient = new HttpClient())
        {
            try
            {
                imageBytes = httpClient.GetByteArrayAsync(imageUrl).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download image: {ex.Message}");
                return;
            }
        }

        using (var imageStream = new MemoryStream(imageBytes))
        using (var fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
        {
            // Bind the source PDF document
            fileStamp.BindPdf(inputPdf);

            // Create a new stamp and bind the downloaded image
            var stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imageStream);

            // Position the stamp on the page (origin is from the lower‑left corner)
            stamp.SetOrigin(100f, 500f);          // X = 100, Y = 500 (adjust as needed)
            stamp.SetImageSize(150f, 50f);        // Width = 150, Height = 50 (adjust as needed)

            // Apply the stamp only to page 2 (pages are 1‑based)
            stamp.Pages = new int[] { 2 };

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save the modified PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}