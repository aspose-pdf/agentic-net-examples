using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "result.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        bool containsImages;

        // Use PdfExtractor (which implements IDisposable) inside a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdfPath);

            // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // Image extraction is performed by calling ExtractImage(), which extracts all images by default.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // removed for compatibility

            // Extract images from the whole document (or set StartPage/EndPage if you need a range)
            extractor.ExtractImage();

            // Determine whether any images were found. HasNextImage() returns true if at least one image is available.
            containsImages = extractor.HasNextImage();
        }

        // Create an anonymous object to hold the result
        var result = new { ContainsImages = containsImages };

        // Serialize the result to JSON with indentation
        string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to the output file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Image detection completed. Result written to '{outputJsonPath}'.");
    }
}
