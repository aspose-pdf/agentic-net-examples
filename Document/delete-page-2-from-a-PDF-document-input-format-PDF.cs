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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Delete page number 2 (pages are 1‑based)
            doc.Pages.Delete(2);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 2 deleted. Result saved to '{outputPath}'.");
    }
}