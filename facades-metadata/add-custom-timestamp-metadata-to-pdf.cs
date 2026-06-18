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

        // Load the PDF using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set a custom metadata field "LastUpdated" with the current UTC timestamp
            string utcNow = DateTime.UtcNow.ToString("o"); // ISO 8601 format
            pdfInfo.SetMetaInfo("LastUpdated", utcNow);

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save the updated PDF.");
                return;
            }
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}