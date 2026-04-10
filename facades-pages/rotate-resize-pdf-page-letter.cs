using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Edit page 3: rotate 90° and set size to Letter in a single operation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF
            editor.BindPdf(inputPath);

            // Apply changes only to page 3 (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };

            // Rotate the selected page(s) by 90 degrees clockwise
            editor.Rotation = 90;

            // Change the page size of the selected page(s) to Letter.
            // The PageSize class does not expose a static "Letter" member in all versions,
            // so create the size explicitly (Letter = 8.5" x 11" = 612 x 792 points).
            editor.PageSize = new PageSize(612, 792);

            // Commit the modifications
            editor.ApplyChanges();

            // Save the result
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
