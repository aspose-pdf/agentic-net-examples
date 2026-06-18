using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // List to hold Base64 strings of extracted images
        List<string> base64Images = new List<string>();

        // PdfExtractor implements IDisposable, wrap in using for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to extract images
            extractor.ExtractImage();

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // MemoryStream will receive the image data (default format is JPEG)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);

                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());
                    base64Images.Add(base64);
                }
            }
        }

        // Serialize the list of Base64 strings to JSON for transmission
        string json = JsonSerializer.Serialize(base64Images);
        Console.WriteLine(json);
    }
}