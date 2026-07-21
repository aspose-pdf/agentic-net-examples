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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to a page editor
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Select non‑consecutive pages (1‑based indexing)
                editor.ProcessPages = new int[] { 1, 3, 5 };

                // Apply a common zoom factor (e.g., 150%) – float literal required
                editor.Zoom = 1.5f;

                // Apply the changes to the bound document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Selected pages zoomed and saved to '{outputPath}'.");
    }
}
