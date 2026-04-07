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

        // Load the PDF, flatten the first page to remove all form fields
        using (Document doc = new Document(inputPath))
        {
            // Page indexing is 1‑based; flatten removes fields and makes their values static
            doc.Pages[1].Flatten();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page cleaned and saved to '{outputPath}'.");
    }
}