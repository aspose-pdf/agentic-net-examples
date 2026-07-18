using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_a3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Set the output page size to A3 (420 x 297 mm)
                editor.PageSize = PageSize.A3;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with A3 page size to '{outputPath}'.");
    }
}