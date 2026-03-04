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
            // Open the existing PDF document using a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                Console.WriteLine($"Opened PDF: {inputPath}");
                // Page indexing in Aspose.Pdf is 1‑based; Pages.Count gives total pages
                Console.WriteLine($"Page count: {doc.Pages.Count}");
                // Example of accessing document metadata
                Console.WriteLine($"Title: {doc.Info.Title}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error opening PDF: {ex.Message}");
        }
    }
}