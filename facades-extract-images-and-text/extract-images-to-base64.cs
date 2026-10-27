using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputJsonPath = "images.json";     // JSON with base64 strings

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable – wrap in using for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            List<string> base64Images = new List<string>();

            // Iterate over each extracted image
            while (extractor.HasNextImage())
            {
                // GetNextImage writes the image data into the provided stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    // Convert the raw bytes to a Base64 string for JSON transmission
                    string base64 = Convert.ToBase64String(imageStream.ToArray());
                    base64Images.Add(base64);
                }
            }

            // Serialize the list of Base64 strings to JSON
            string json = JsonSerializer.Serialize(base64Images, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Extracted {base64Images.Count} images and saved to '{outputJsonPath}'.");
        }
    }
}