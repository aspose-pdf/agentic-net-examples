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

        // Open the PDF with PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Load the existing PDF
            pdfInfo.BindPdf(inputPath);

            // Add custom metadata field "LastUpdated" with current UTC time (ISO 8601)
            string utcNow = DateTime.UtcNow.ToString("o");
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