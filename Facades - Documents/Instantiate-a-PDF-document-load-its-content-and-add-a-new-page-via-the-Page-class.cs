using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to a PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Add a new blank page to the document (1‑based indexing)
                Page newPage = doc.Pages.Add();

                // Optionally set page size (A4 in points)
                newPage.PageInfo.Width  = 595; // 8.27 inches * 72
                newPage.PageInfo.Height = 842; // 11.69 inches * 72

                // Apply any changes made via the editor (required for some operations)
                editor.ApplyChanges();

                // Save the modified document using the facade's Save method
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with new page to '{outputPath}'.");
    }
}