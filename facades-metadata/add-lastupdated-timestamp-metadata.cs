using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Add custom metadata field "LastUpdated" with current UTC timestamp (ISO 8601)
            string utcNow = DateTime.UtcNow.ToString("o");
            pdfInfo.SetMetaInfo("LastUpdated", utcNow);

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata updated and saved to '{outputPath}'."
                : $"Failed to save updated PDF to '{outputPath}'.");
        }
    }
}