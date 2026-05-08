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

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete the first page (page numbers are 1‑based)
            doc.Pages.Delete(1);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page deleted. Output saved to '{outputPath}'.");
    }
}