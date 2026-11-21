using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string metaKey    = "ObsoleteField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF file to the PdfFileInfo facade
            PdfFileInfo pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputPath);

            // Set the custom metadata entry to an empty string (effectively removing it)
            pdfInfo.SetMetaInfo(metaKey, string.Empty);

            // Save the modified PDF
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata \"{metaKey}\" cleared and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}