using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable via SaveableFacade, so use a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Initialize the facade with the PDF file.
            pdfInfo.BindPdf(inputPdf);

            // Retrieve the PDF version string.
            string pdfVersion = pdfInfo.GetPdfVersion();

            // Store the version for later use (here we just display it).
            Console.WriteLine($"PDF Version: {pdfVersion}");

            // The variable pdfVersion can be used later in the program as needed.
        }
    }
}