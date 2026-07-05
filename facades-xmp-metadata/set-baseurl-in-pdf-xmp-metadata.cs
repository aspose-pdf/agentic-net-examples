using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string baseUrl    = "https://www.example.com/";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind to the PDF and modify its XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Set the BaseURL property (xmp:BaseURL) in the XMP metadata
            xmp.Add("xmp:BaseURL", baseUrl);

            // Save the updated PDF with the new metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"BaseURL set to '{baseUrl}' and saved as '{outputPath}'.");
    }
}