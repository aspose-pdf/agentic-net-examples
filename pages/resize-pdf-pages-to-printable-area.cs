using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the printable area (595 × 842 points)
            PageSize targetSize = new PageSize(595f, 842f);

            // Resize each page; Resize scales content proportionally
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Resize(targetSize);
            }

            // Save the resized document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}