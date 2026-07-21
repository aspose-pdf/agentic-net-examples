using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "customsize.pdf";

        // Desired custom page dimensions (points; 1 inch = 72 points)
        double customWidth = 500;   // e.g., 500 points wide
        double customHeight = 800;  // e.g., 800 points high

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to edit page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Set a custom page size for the output document
            editor.PageSize = new PageSize((float)customWidth, (float)customHeight);

            // Apply the changes to all pages (default ProcessPages = all)
            editor.ApplyChanges();

            // Save the modified PDF with the new page dimensions
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}