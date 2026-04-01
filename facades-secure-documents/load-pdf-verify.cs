using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document document = new Document(inputPath))
            {
                int pageCount = document.Pages.Count;
                Console.WriteLine($"PDF loaded successfully. Page count: {pageCount}");
                // Save to demonstrate that the document is usable after loading
                document.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading PDF: {ex.Message}");
        }
    }
}