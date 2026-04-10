using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    // Simple DTO to hold image data and basic metadata
    private class ImageInfo
    {
        public int Index { get; set; }
        // Initialise with empty string to satisfy non‑nullable reference type analysis
        public string Base64 { get; set; } = string.Empty;
        public long ByteSize { get; set; }
        // Initialise with empty string; will be overwritten when the image is processed
        public string Format { get; set; } = string.Empty;
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Path to the source PDF
        const string outputJsonPath = "images.json";      // Path where the JSON will be written

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // List that will hold information for all extracted images
        List<ImageInfo> images = new List<ImageInfo>();

        // Use the PdfExtractor facade to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Start the image extraction process
            extractor.ExtractImage();

            int index = 1;
            // Iterate over all images found in the document
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream (default format is JPEG)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    // Ensure the stream is ready for reading
                    imageStream.Position = 0;

                    // Convert the raw bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());

                    // Populate the DTO with metadata
                    ImageInfo info = new ImageInfo
                    {
                        Index = index,
                        Base64 = base64,
                        ByteSize = imageStream.Length,
                        Format = "JPEG" // GetNextImage without format argument returns JPEG
                    };

                    images.Add(info);
                }

                index++;
            }
        }

        // Serialize the list to a nicely formatted JSON string
        string json = JsonSerializer.Serialize(images, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON to the output file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Extracted {images.Count} images and saved JSON to '{outputJsonPath}'.");
    }
}
