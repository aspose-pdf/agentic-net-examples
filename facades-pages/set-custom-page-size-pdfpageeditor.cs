using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "custom_page.pdf";

        // Custom dimensions in points (1 inch = 72 points)
        double customWidth  = 500; // e.g., 500 points wide
        double customHeight = 800; // e.g., 800 points high

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor, set the new size, apply changes, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Set the desired page size for the output document.
            editor.PageSize = new PageSize((float)customWidth, (float)customHeight);

            // Apply the size change to all pages.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}