using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Preserve existing custom metadata and add a new field "Version"
            pdfInfo.SetMetaInfo("Version", "1.0");

            // Save the updated PDF (other metadata remains unchanged)
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata updated successfully. Saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}