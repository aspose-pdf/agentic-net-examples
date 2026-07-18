using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect odd‑numbered page indexes (1‑based)
            int pageCount = doc.Pages.Count;
            List<int> oddPages = new List<int>();
            for (int i = 1; i <= pageCount; i += 2)
                oddPages.Add(i);

            // Initialize the page editor with the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Apply changes only to odd pages
                editor.ProcessPages = oddPages.ToArray();

                // Set zoom factor (1.0 = 100%)
                editor.Zoom = 1.2f;

                // Apply the zoom changes
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Zoomed PDF saved to '{outputPath}'.");
    }
}