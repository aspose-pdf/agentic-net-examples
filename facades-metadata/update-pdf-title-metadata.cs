using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "Updated PDF Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade with the source PDF
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update the Title metadata
            pdfInfo.Title = newTitle;

            // Save the updated PDF to a new file. SaveNewInfo returns void, so we just call it.
            pdfInfo.SaveNewInfo(outputPath);

            // Optionally verify that the file was created
            bool saved = File.Exists(outputPath);
            Console.WriteLine(saved
                ? $"Title updated and saved to '{outputPath}'."
                : $"Failed to save updated PDF to '{outputPath}'.");
        }
    }
}
