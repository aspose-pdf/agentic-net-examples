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

        // Initialize PdfFileInfo with the PDF file path.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the PDF version string.
            string pdfVersion = pdfInfo.GetPdfVersion();

            // Example usage: display the version.
            Console.WriteLine($"PDF version: {pdfVersion}");

            // The variable 'pdfVersion' can be stored or passed to other methods as needed.
        }
    }
}