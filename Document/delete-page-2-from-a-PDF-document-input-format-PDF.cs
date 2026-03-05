using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, delete page 2, and save the result.
        using (Document doc = new Document(inputPath))
        {
            // Page numbers are 1‑based; delete the second page.
            doc.Pages.Delete(2);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 2 deleted. Result saved to '{outputPath}'.");
    }
}