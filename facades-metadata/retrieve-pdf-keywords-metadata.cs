using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the Keywords property
            string keywords = pdfInfo.Keywords;

            // Output the Keywords value (empty if not set)
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}