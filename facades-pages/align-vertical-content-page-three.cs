using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor, load the PDF, adjust alignment, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Edit only page 3 (1‑based indexing).
            editor.ProcessPages = new int[] { 3 };

            // Align the original content vertically to the middle of the page.
            // The correct enum value is Center (not Middle).
            editor.VerticalAlignmentType = VerticalAlignment.Center;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied to page 3. Saved as '{outputPath}'.");
    }
}
