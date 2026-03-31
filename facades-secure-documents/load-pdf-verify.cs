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
            using (Document pdfDocument = new Document(inputPath))
            {
                int pageCount = pdfDocument.Pages.Count;
                Console.WriteLine($"PDF loaded successfully. Page count: {pageCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading PDF: {ex.Message}");
        }
    }
}
