using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    // Simple DTO for JSON output
    private class ImageInfo
    {
        public int Index { get; set; }          // Sequential index of the image
        public string Base64 { get; set; }      // Base64 representation of the image bytes
        public string Format { get; set; }      // Image format (default is JPEG)
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";          // Path to the source PDF
        const string outputJson = "images.json";      // Path where JSON will be written

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var images = new List<ImageInfo>();
        int imageIndex = 1;

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Instruct the extractor to process images
            extractor.ExtractImage();

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream (default format is JPEG)
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);

                    // Ensure the stream position is at the beginning before reading
                    imgStream.Position = 0;

                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imgStream.ToArray());

                    // Store metadata and the Base64 data
                    images.Add(new ImageInfo
                    {
                        Index = imageIndex++,
                        Base64 = base64,
                        Format = "jpeg"   // Default format used by GetNextImage()
                    });
                }
            }
        }

        // Serialize the list of images to JSON
        string json = JsonSerializer.Serialize(images, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to the output file
        File.WriteAllText(outputJson, json, Encoding.UTF8);

        Console.WriteLine($"Extracted {images.Count} image(s) and saved JSON to '{outputJson}'.");
    }
}