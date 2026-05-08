using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Facade API to bind the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);               // Load the PDF
            Document doc = pdfInfo.Document;          // Retrieve the underlying Document

            // Verify that the document was loaded successfully
            if (doc != null && doc.Pages.Count > 0)
            {
                Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
            }
            else
            {
                Console.Error.WriteLine("Failed to load PDF or the document contains no pages.");
            }
        }
    }
}