using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade with the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the PDF version string
            string pdfVersion = pdfInfo.GetPdfVersion();

            // Store the version for later use (example: display it)
            Console.WriteLine($"PDF version: {pdfVersion}");
            // The variable 'pdfVersion' can now be used elsewhere in the program
        }
    }
}