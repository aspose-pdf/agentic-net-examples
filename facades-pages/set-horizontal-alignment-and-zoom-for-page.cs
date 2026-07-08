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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade to modify page properties
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the editor to the loaded document
                editor.BindPdf(doc);

                // Target only page 4 (1‑based indexing)
                editor.ProcessPages = new int[] { 4 };

                // Set horizontal alignment to center for better readability
                editor.HorizontalAlignment = HorizontalAlignment.Center;

                // Set zoom level to 1.2 (120%)
                editor.Zoom = 1.2f;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}