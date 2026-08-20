using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Edit page 3: rotate 90° and set size to Letter in a single operation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);                 // Load source PDF
            editor.ProcessPages = new int[] { 3 };     // Target only page 3
            editor.Rotation = 90;                      // Rotate 90 degrees
            // Letter size = 8.5" x 11" = 612 x 792 points
            editor.PageSize = new Aspose.Pdf.PageSize(612, 792);
            editor.ApplyChanges();                     // Apply modifications
            editor.Save(outputPath);                   // Save result
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
