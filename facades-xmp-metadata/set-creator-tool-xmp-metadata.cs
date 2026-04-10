using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Determine the current application version
        string appVersion = typeof(Program).Assembly.GetName().Version?.ToString() ?? "1.0.0";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use a using block to ensure resources are released promptly
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF
            xmp.BindPdf(inputPath);

            // Set the xmp:CreatorTool property to the application version
            // The Add method expects the property name without a namespace prefix;
            // Aspose.Pdf automatically maps "CreatorTool" to the correct XMP namespace.
            xmp.Add("CreatorTool", appVersion);

            // Save the updated PDF with the new XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"CreatorTool set to '{appVersion}' and saved to '{outputPath}'.");
    }
}
