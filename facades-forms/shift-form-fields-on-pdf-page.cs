using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "fields_moved.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to shift page content (including form fields) on page 2
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Restrict editing to page 2 only (1‑based indexing)
            editor.ProcessPages = new int[] { 2 };

            // Move the origin 5 points to the right (X axis), no vertical shift
            editor.MovePosition(5f, 0f);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Fields on page 2 moved right by 5 points. Saved to '{outputPath}'.");
    }
}