using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "images.json";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // List to hold image metadata and base64 data
        var imageList = new List<object>();

        // Use PdfExtractor to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream in PNG format
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // The ImageFormat enum lives in System.Drawing.Imaging
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
                    byte[] imageBytes = imageStream.ToArray();

                    // Convert image bytes to a Base64 string
                    string base64Data = Convert.ToBase64String(imageBytes);

                    // Add metadata and Base64 data to the list
                    imageList.Add(new
                    {
                        Index = imageIndex,
                        Format = "png",
                        Data = base64Data
                    });
                }

                imageIndex++;
            }
        }

        // Serialize the list to a formatted JSON string
        string jsonOutput = JsonSerializer.Serialize(imageList, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON to the output file
        File.WriteAllText(outputJsonPath, jsonOutput);

        Console.WriteLine($"Extracted {imageList.Count} images to '{outputJsonPath}'.");
    }
}