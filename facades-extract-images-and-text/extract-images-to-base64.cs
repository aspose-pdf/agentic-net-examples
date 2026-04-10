using System;
using System.IO;
using System.Collections.Generic;
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

        // PdfExtractor implements IDisposable, use using for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to work with images
            extractor.ExtractImage();

            // Iterate over all images in the PDF
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream (default format is JPEG)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // reset position before reading

                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());
                    base64Images.Add(base64);
                }
            }
        }

        // Serialize the list of Base64 strings to JSON for transmission
        string jsonPayload = JsonSerializer.Serialize(base64Images);
        Console.WriteLine(jsonPayload);
    }
}