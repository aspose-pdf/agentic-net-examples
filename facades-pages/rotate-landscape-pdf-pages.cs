using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and bind it to PdfPageEditor
        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Get page dimensions
                var page = doc.Pages[i];
                double width = page.Rect.Width;
                double height = page.Rect.Height;

                // If the page is landscape (width > height), rotate it 90° to portrait
                if (width > height)
                {
                    editor.Rotation = 90;                 // Valid values: 0, 90, 180, 270
                    editor.ProcessPages = new int[] { i }; // Apply only to this page
                    editor.ApplyChanges();                // Commit the rotation
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}