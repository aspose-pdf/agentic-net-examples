using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "image_presence.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        bool hasImage = false;

        // PdfExtractor implements IDisposable; ensure deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);
            // Enable image extraction – in recent Aspose.Pdf versions this is a method, not a property
            extractor.ExtractImage();

            // Check if any image exists
            while (extractor.HasNextImage())
            {
                hasImage = true;
                break; // No need to continue after first detection
            }
        }

        // Write the detection result to a text file
        try
        {
            string result = hasImage ? "Images found" : "No images found";
            File.WriteAllText(outputTxt, result);
            Console.WriteLine($"Result written to {outputTxt}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write output: {ex.Message}");
        }
    }
}
