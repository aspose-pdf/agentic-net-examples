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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page properties such as rotation, size, and zoom.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Apply modifications to all pages (default behavior).
            // Rotate pages by 90 degrees.
            editor.Rotation = 90;

            // Change page size to A4.
            editor.PageSize = PageSize.A4;

            // Set zoom factor (1.0 = 100%).
            editor.Zoom = 0.75f;

            // Optionally move the origin of the content.
            editor.MovePosition(20f, 30f);

            // Commit the changes.
            editor.ApplyChanges();

            // Save the edited PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}