using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facade API (PdfPageEditor)

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
            // Bind the document to a PdfPageEditor facade (required by the task)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Add a new blank page to the document (pages are 1‑based)
                Page newPage = doc.Pages.Add();

                // Optionally set the page size (A4 in points)
                newPage.PageInfo.Width  = 595; // 8.27 inches * 72
                newPage.PageInfo.Height = 842; // 11.69 inches * 72

                // Apply any changes made via the editor (required to commit edits)
                editor.ApplyChanges();

                // Save the modified document (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with new page: {outputPath}");
    }
}