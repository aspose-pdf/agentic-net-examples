using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Get total number of pages (1‑based indexing)
        int pageCount = pdfDocument.Pages.Count;

        // Rotate every third page by 270° (landscape)
        for (int i = 1; i <= pageCount; i++)
        {
            if (i % 3 == 0)
            {
                pdfDocument.Pages[i].Rotate = Rotation.on270;
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
