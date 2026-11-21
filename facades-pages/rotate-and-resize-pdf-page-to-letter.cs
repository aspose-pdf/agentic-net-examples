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

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Page numbers are 1‑based. Target page 3.
        int pageNumber = 3;
        Page page = pdf.Pages[pageNumber];

        // Rotate the page 90 degrees clockwise.
        page.Rotate = Rotation.on90;

        // Change the page size to Letter (8.5" x 11" = 612 x 792 points).
        page.SetPageSize(612, 792);

        // Save the modified document.
        pdf.Save(outputPath);

        Console.WriteLine($"Page {pageNumber} rotated 90° and resized to Letter saved as '{outputPath}'.");
    }
}
