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
            // Get the PDF version string
            string pdfVersion = pdfInfo.GetPdfVersion();

            // Example usage: display the version
            Console.WriteLine($"PDF version: {pdfVersion}");

            // The variable pdfVersion can be used later in the program as needed
        }
    }
}