using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // List to hold Base64 strings of extracted images
        List<string> base64Images = new List<string>();

        // Use PdfExtractor (implements IDisposable) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdfPath);

            // Prepare for image extraction
            extractor.ExtractImage();

            // Iterate through all images in the PDF
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);

                    // Convert the stream content to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());
                    base64Images.Add(base64);
                }
            }

            // Release resources held by the extractor
            extractor.Close();
        }

        // Serialize the list of Base64 strings to JSON for transmission
        string jsonPayload = JsonSerializer.Serialize(base64Images);
        Console.WriteLine(jsonPayload);
    }
}