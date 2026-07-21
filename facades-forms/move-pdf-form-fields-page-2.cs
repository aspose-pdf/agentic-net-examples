using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_moved_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor can shift the origin of selected pages.
        // Setting ProcessPages to page 2 (1‑based index) and moving X by 5 points.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.ProcessPages = new int[] { 2 };   // Target only page 2
            editor.MovePosition(5f, 0f);             // Shift right by 5 points
            editor.Save(outputPath);                 // Save the modified PDF
        }

        Console.WriteLine($"Fields on page 2 moved right by 5 points. Saved to '{outputPath}'.");
    }
}