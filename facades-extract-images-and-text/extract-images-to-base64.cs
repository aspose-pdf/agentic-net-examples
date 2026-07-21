using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect Base64 strings of extracted images
        List<string> base64Images = new List<string>();

        // PdfExtractor implements IDisposable; wrap in using for proper disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPath);

            // Prepare the extractor to work with images
            extractor.ExtractImage();

            // Iterate through all images in the document
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Retrieve the next image (default format is JPEG)
                    bool success = extractor.GetNextImage(ms);
                    if (success)
                    {
                        // Reset stream position before reading
                        ms.Position = 0;

                        // Convert the image bytes to a Base64 string
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        base64Images.Add(base64);
                    }
                }
            }
        }

        // Serialize the list of Base64 strings to JSON for transmission
        string json = JsonSerializer.Serialize(base64Images);
        Console.WriteLine(json);
    }
}