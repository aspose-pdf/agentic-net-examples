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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Download the image into a memory stream
        byte[] imageBytes;
        using (HttpClient client = new HttpClient())
        {
            try
            {
                imageBytes = client.GetByteArrayAsync(imageUrl).Result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download image: {ex.Message}");
                return;
            }
        }

        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an ImageStamp from the stream
            ImageStamp imgStamp = new ImageStamp(imgStream);

            // Configure stamp appearance and position
            imgStamp.Width = 100;                     // width in points
            imgStamp.Height = 100;                    // height in points
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment = VerticalAlignment.Top;
            imgStamp.TopMargin = 50;                  // distance from top edge

            // Apply the stamp to page 2 (pages are 1‑based)
            pdfDoc.Pages[2].AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}