using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "images.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var images = new List<ImageInfo>();

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: set higher resolution for clearer images
            // extractor.Resolution = 300;
            extractor.ExtractImage();

            int index = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Store the next image in a memory stream (default JPEG format)
                    extractor.GetNextImage(ms);
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    images.Add(new ImageInfo
                    {
                        Index = index,
                        Base64 = base64,
                        Format = "jpeg"
                    });
                }
                index++;
            }
        }

        // Serialize the collection to a formatted JSON file
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(images, jsonOptions);
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Extracted {images.Count} images to '{outputJson}'.");
    }

    // Simple DTO for JSON output
    private class ImageInfo
    {
        public int Index { get; set; }
        public string Base64 { get; set; }
        public string Format { get; set; }
    }
}