using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        //   0 - path to the source PDF file
        //   1 - base URL to write into the XMP metadata (e.g. https://example.com)
        //   2 - optional path for the output PDF; if omitted a file named "updated.pdf" is created next to the source file.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <baseUrl> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        string baseUrl = args[1];
        string outputPath = args.Length > 2 ? args[2] : Path.Combine(Path.GetDirectoryName(inputPath) ?? ".", "updated.pdf");

        try
        {
            UpdateXmpBaseUrl(inputPath, baseUrl, outputPath);
            Console.WriteLine($"XMP BaseURL updated successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads a PDF, sets the XMP "xmp:BaseURL" property to the supplied value, and saves the result.
    /// </summary>
    static void UpdateXmpBaseUrl(string inputPath, string baseUrl, string outputPath)
    {
        // Load the PDF into the XMP metadata helper.
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
        xmpMetadata.BindPdf(inputPath);

        // If a BaseURL already exists, remove it before adding the new value.
        if (xmpMetadata.Contains("xmp:BaseURL"))
        {
            xmpMetadata.Remove("xmp:BaseURL");
        }

        // Add the new BaseURL.
        xmpMetadata.Add("xmp:BaseURL", baseUrl);

        // Save the updated PDF.
        xmpMetadata.Save(outputPath);
    }
}
