using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF file using PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf);

        // Set custom metadata field "LastUpdated" with current UTC timestamp (ISO 8601 format)
        string utcNow = DateTime.UtcNow.ToString("o");
        pdfInfo.SetMetaInfo("LastUpdated", utcNow);

        // Save the updated PDF to a new file
        bool success = pdfInfo.SaveNewInfo(outputPdf);
        if (!success)
        {
            Console.Error.WriteLine("Failed to save updated PDF.");
            return;
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'. Custom field 'LastUpdated' set to {utcNow}.");
    }
}