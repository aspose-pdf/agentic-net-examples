using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string creator   = "My Custom Creator";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF metadata via PdfFileInfo
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf);

        // Assign a custom Creator value
        pdfInfo.Creator = creator;

        // Persist the updated metadata to a new PDF file
        bool saved = pdfInfo.SaveNewInfo(outputPdf);
        if (!saved)
        {
            Console.Error.WriteLine("Failed to save the updated PDF.");
        }
        else
        {
            Console.WriteLine($"Creator set to \"{creator}\" and saved as \"{outputPdf}\".");
        }

        // Release resources held by the facade
        pdfInfo.Close();
    }
}