using System;
using System.IO;
using Aspose.Pdf;

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
            // Load the PDF using Aspose.Pdf.Document (replaces the obsolete Facade PdfDocument)
            using (var pdfDoc = new Document(inputPath))
            {
                // Verify successful loading by checking the page count
                int pageCount = pdfDoc?.Pages?.Count ?? 0;
                Console.WriteLine($"PDF loaded successfully. Page count: {pageCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading PDF: {ex.Message}");
        }
    }
}