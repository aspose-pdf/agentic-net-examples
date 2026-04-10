using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the beginning (page numbers are 1‑based)
            Page newPage = doc.Pages.Insert(1);

            // Set the custom size: 200 × 300 points
            newPage.SetPageSize(200, 300);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page at '{outputPath}'.");
    }
}