using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Specify non‑consecutive pages to be edited (1‑based indexing)
                editor.ProcessPages = new int[] { 1, 3, 5 }; // pages 1, 3 and 5

                // Apply a common zoom factor to the selected pages (e.g., 150%)
                editor.Zoom = 1.5f; // 1.0f = 100%

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified document to the output file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom applied to selected pages. Output saved to '{outputPath}'.");
    }
}
