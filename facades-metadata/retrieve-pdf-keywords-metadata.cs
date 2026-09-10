using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfFileInfo facade for the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the Keywords metadata (empty string if not set)
            string keywords = pdfInfo.Keywords;

            // Display the retrieved value
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}