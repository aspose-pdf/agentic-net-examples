using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int    pageNumber = 1; // page to rotate (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade with the loaded document.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Apply a 180° rotation to the specified page.
                // Using PageRotations preserves any existing transition settings.
                editor.PageRotations = new Dictionary<int, int> { { pageNumber, 180 } };

                // Commit the changes and save the result.
                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page {pageNumber} rotated 180° and saved to '{outputPath}'.");
    }
}