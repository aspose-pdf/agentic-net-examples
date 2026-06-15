using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_a4_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // A4 size in points: 210 mm × 297 mm ≈ 595 pt × 842 pt.
                // Landscape orientation: width = 842 pt, height = 595 pt.
                double width  = 842; // points
                double height = 595; // points

                // Set custom page size using Width and Height properties
                editor.PageSize = new PageSize((float)width, (float)height);

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom A4 landscape size to '{outputPath}'.");
    }
}