using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Set the desired page size (e.g., A4). This changes the page dimensions
                // while keeping existing content, annotations, form fields, links, etc.
                editor.PageSize = Aspose.Pdf.PageSize.A4;

                // Process all pages (null means all pages). Adjust if you need a subset.
                editor.ProcessPages = null;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document. No SaveOptions needed because we keep PDF format.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}