using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string versionValue = "1.0";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF for metadata manipulation
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);

        // Add or update the "Version" metadata field; existing metadata remains unchanged
        pdfInfo.SetMetaInfo("Version", versionValue);

        // Save the PDF with the updated metadata
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}