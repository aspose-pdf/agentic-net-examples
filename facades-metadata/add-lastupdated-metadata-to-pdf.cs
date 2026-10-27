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

        try
        {
            // Load the PDF using the Facades API (no full Document load required)
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

            // Set a custom metadata field "LastUpdated" with the current UTC timestamp
            string utcNow = DateTime.UtcNow.ToString("o"); // ISO 8601 format
            pdfInfo.SetMetaInfo("LastUpdated", utcNow);

            // Save the updated PDF to a new file
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata updated. 'LastUpdated' = {utcNow}");
            Console.WriteLine($"Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}