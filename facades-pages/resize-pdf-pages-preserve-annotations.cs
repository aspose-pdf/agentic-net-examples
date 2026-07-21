using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
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

                // Set the target page size (e.g., A4). This changes the page dimensions
                // while preserving existing content, including annotations.
                editor.PageSize = Aspose.Pdf.PageSize.A4;

                // Apply the changes to all pages (ProcessPages null means all pages)
                editor.ProcessPages = null;
                editor.ApplyChanges();

                // Save the modified document
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}