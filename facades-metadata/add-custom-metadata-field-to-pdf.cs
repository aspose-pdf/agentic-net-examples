using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string version    = "1.0";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load existing PDF metadata using the Facades API
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Add or update the custom field "Version". All other metadata entries are preserved.
        pdfInfo.SetMetaInfo("Version", version);

        // Save the PDF with the updated metadata
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata updated. Output saved to '{outputPath}'.");
    }
}