using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cropped.pdf";
        const int pageNumber = 1; // 1‑based page index

        // Desired MediaBox coordinates (left, bottom, right, top)
        double llx = 50;
        double lly = 50;
        double urx = 550;
        double ury = 750;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify the requested page exists
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            // Set a custom MediaBox; the page content is not altered
            doc.Pages[pageNumber].MediaBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}