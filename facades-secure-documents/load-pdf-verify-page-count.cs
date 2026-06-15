using System;
using System.IO;
using Aspose.Pdf;
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

        try
        {
            // Load the PDF using the PdfFileInfo facade
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(inputPath);               // Initialize the facade with the file
                Document doc = pdfInfo.Document;          // Access the underlying Document object

                // Verify successful loading by checking the page count
                if (doc != null && doc.Pages.Count > 0)
                {
                    Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("PDF loaded but contains no pages.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading PDF: {ex.Message}");
        }
    }
}