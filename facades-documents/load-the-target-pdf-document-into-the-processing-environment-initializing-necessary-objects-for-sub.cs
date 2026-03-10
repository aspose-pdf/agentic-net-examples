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

        // Load the PDF document directly. PdfViewer does not expose a Document property.
        try
        {
            using (Document doc = new Document(inputPath))
            {
                Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
                // Additional processing can be performed here
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
        }
    }
}
