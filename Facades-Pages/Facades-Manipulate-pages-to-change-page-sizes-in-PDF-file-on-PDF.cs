using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the PdfPageEditor facade which allows page size manipulation
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Set the desired output page size (e.g., A4). This will be applied to all pages.
                editor.PageSize = PageSize.A4;

                // Optional: specify a subset of pages to process.
                // editor.ProcessPages = new int[] { 1, 2, 3 };

                // Apply the size changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document using the standard Document.Save method
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}