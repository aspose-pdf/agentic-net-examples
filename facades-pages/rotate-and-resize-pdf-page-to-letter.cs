using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Rotation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Ensure the document has at least three pages
        if (pdfDocument.Pages.Count < 3)
        {
            Console.Error.WriteLine("The PDF does not contain a third page.");
            return;
        }

        // Get page 3 (pages are 1‑based)
        Page page = pdfDocument.Pages[3];

        // Rotate page 3 by 90 degrees clockwise and resize to Letter (612 x 792 points)
        page.Rotate = Rotation.on90;
        page.SetPageSize(612, 792);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Page 3 rotated 90° and resized to Letter saved as '{outputPath}'.");
    }
}
