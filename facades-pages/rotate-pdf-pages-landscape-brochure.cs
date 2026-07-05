using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "brochure_output.pdf"; // rotated PDF for printing

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a Facade that allows page‑level modifications such as rotation and size.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Rotate all pages 90 degrees clockwise to obtain landscape orientation.
            // Valid values are 0, 90, 180, 270.
            editor.Rotation = 90;

            // Set the output page size to A4. After rotation, A4 will be landscape (842 × 595 points).
            editor.PageSize = Aspose.Pdf.PageSize.A4;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);

            // Close releases any resources held by the editor.
            editor.Close();
        }

        Console.WriteLine($"Brochure‑ready PDF saved to '{outputPath}'.");
    }
}