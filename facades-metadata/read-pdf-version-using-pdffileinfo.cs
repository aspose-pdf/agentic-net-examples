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

        // Variable to hold the PDF version string
        string pdfVersion;

        // PdfFileInfo implements IDisposable; use using for proper disposal
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the version information (e.g., "1.7")
            pdfVersion = fileInfo.GetPdfVersion();
        }

        // The version can now be used elsewhere in the program
        Console.WriteLine($"PDF version: {pdfVersion}");
    }
}