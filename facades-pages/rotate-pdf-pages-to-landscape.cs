using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "brochure_input.pdf";
        const string outputPdf = "brochure_landscape.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfPageEditor facade (facade API for page manipulation)
        PdfPageEditor editor = new PdfPageEditor();

        // Load the source PDF file into the facade
        editor.BindPdf(inputPdf);

        // Rotate all pages 90 degrees clockwise (portrait → landscape)
        editor.Rotation = 90; // valid values: 0, 90, 180, 270

        // Set the output page size to standard A4 (brochure size)
        // A4 is 210 mm × 297 mm; with a 90° rotation it becomes landscape.
        editor.PageSize = PageSize.A4;

        // Apply the rotation and page‑size changes
        editor.ApplyChanges();

        // Save the transformed PDF
        editor.Save(outputPdf);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Rotated brochure saved to '{outputPdf}'.");
    }
}