using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Specify that only page 7 should be edited (pages are 1‑based)
                editor.ProcessPages = new int[] { 7 };

                // Set zoom factor to 2.0 (200% magnification)
                editor.Zoom = 2.0f;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 zoom set to 2.0 and saved as '{outputPath}'.");
    }
}