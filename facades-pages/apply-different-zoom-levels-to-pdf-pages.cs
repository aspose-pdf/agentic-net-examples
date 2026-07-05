using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Example: set different zoom levels per page
                    // Odd pages -> 50% zoom, Even pages -> 100% zoom
                    float zoom = (i % 2 == 1) ? 0.5f : 1.0f;

                    // Specify the page to edit
                    editor.ProcessPages = new int[] { i };

                    // Apply the zoom factor
                    editor.Zoom = zoom;

                    // Commit changes for this page
                    editor.ApplyChanges();
                }

                // Save the modified PDF (using rule for saving)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom levels applied and saved to '{outputPath}'.");
    }
}