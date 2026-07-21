using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades API (PdfPageEditor)

class RotateForBrochure
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "brochure_output.pdf"; // rotated PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade to modify page layout
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the editor to the loaded document
            editor.BindPdf(doc);

            // Rotate all pages 90 degrees clockwise (portrait → landscape)
            editor.Rotation = 90; // valid values: 0, 90, 180, 270

            // Set the output page size to A4. After rotation the page will be A4 landscape.
            editor.PageSize = Aspose.Pdf.PageSize.A4;

            // ProcessPages defaults to all pages; explicit assignment is optional
            // editor.ProcessPages = null;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Brochure-ready PDF saved to '{outputPath}'.");
    }
}