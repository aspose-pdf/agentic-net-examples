using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove all form fields on the first page (page indexing is 1‑based)
            doc.Pages[1].Flatten();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page template saved to '{outputPath}'.");
    }
}