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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileInfo facade to work with PDF metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the existing PDF document
            pdfInfo.BindPdf(inputPath);

            // Set a custom metadata field "LastUpdated" with the current UTC timestamp (ISO 8601 format)
            string utcNow = DateTime.UtcNow.ToString("o");
            pdfInfo.SetMetaInfo("LastUpdated", utcNow);

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }
        }

        Console.WriteLine($"PDF saved with updated metadata to '{outputPath}'.");
    }
}