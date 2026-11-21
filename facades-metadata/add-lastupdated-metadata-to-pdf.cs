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

        // Load the PDF using the Facades API
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Set custom metadata field "LastUpdated" to the current UTC time (ISO 8601 format)
        string utcNow = DateTime.UtcNow.ToString("o");
        pdfInfo.SetMetaInfo("LastUpdated", utcNow);

        // Save the PDF with the updated metadata
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata updated. LastUpdated = {utcNow}");
    }
}