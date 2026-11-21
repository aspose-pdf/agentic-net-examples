using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Initialize the PDF extractor (Facades API)
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF file to the extractor
        extractor.BindPdf(inputPdfPath);

        // Enable image extraction
        extractor.ExtractImage();

        // Collect Base64 strings for each extracted image
        List<string> base64Images = new List<string>();

        // Iterate through all images in the PDF
        while (extractor.HasNextImage())
        {
            // Use a MemoryStream to hold the image data temporarily
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Retrieve the next image into the stream
                extractor.GetNextImage(imageStream);

                // Convert the stream's bytes to a Base64 string
                string base64 = Convert.ToBase64String(imageStream.ToArray());

                base64Images.Add(base64);
            }
        }

        // Serialize the list of Base64 strings to JSON for transmission
        string jsonPayload = JsonSerializer.Serialize(base64Images);

        // Output the JSON (could be sent over a network, written to a file, etc.)
        Console.WriteLine(jsonPayload);
    }
}