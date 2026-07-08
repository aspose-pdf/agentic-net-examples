using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfImageExtraction
{
    // Simple DTO to hold image data and minimal metadata
    public class ImageData
    {
        public int Index { get; set; }          // Sequential index of the extracted image
        public string Format { get; set; }      // Image format (default JPEG for PdfExtractor)
        public string Base64 { get; set; }      // Base64 representation of the image bytes
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";   // Path to the source PDF
            const string outputJsonPath = "images.json"; // Path where JSON will be saved

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"File not found: {inputPdfPath}");
                return;
            }

            // List to accumulate extracted images
            var images = new List<ImageData>();
            int imageIndex = 1;

            // PdfExtractor implements IDisposable, ensure deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor facade
                extractor.BindPdf(inputPdfPath);

                // Initiate image extraction
                extractor.ExtractImage();

                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream);

                        // Ensure the stream position is at the beginning before reading
                        imageStream.Position = 0;

                        // Convert image bytes to Base64 string
                        string base64 = Convert.ToBase64String(imageStream.ToArray());

                        // Populate DTO (PdfExtractor returns JPEG by default)
                        images.Add(new ImageData
                        {
                            Index = imageIndex,
                            Format = "jpeg",
                            Base64 = base64
                        });
                    }

                    imageIndex++;
                }
            }

            // Serialize the list to JSON
            string json = JsonSerializer.Serialize(images, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json, Encoding.UTF8);

            Console.WriteLine($"Extraction complete. JSON saved to '{outputJsonPath}'.");
        }
    }
}