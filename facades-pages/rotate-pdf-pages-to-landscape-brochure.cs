using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "brochure_output.pdf";

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

                // Rotate all pages 90 degrees to landscape orientation
                editor.Rotation = 90; // Valid values: 0, 90, 180, 270

                // Set the output page size to standard A4 (brochure size)
                editor.PageSize = PageSize.A4; // A4 size will be used in landscape after rotation

                // Apply the rotation and page size changes
                editor.ApplyChanges();

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Brochure PDF saved to '{outputPath}'.");
    }
}