using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;

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

        // Download the image from the external URL into a memory stream
        using (HttpClient httpClient = new HttpClient())
        using (Stream imageStream = httpClient.GetStreamAsync(imageUrl).Result)
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // Create an ImageStamp from the downloaded stream
            ImageStamp stamp = new ImageStamp(imageStream)
            {
                // Position the stamp (distance from left and bottom edges)
                XIndent = 100,
                YIndent = 500,
                // Set desired size of the stamp
                Width = 150,
                Height = 50,
                // Place the stamp over existing content
                Background = false
            };

            // Apply the stamp to page 2 (pages are 1‑based)
            pdfDocument.Pages[2].AddStamp(stamp);

            // Save the modified PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}
