using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "modified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the signed PDF. Using block ensures the Document is disposed properly.
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document.
            // This facade works on the same Document instance, allowing incremental updates.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Example modification: rotate page 2 by 90 degrees.
                // Page numbers are 1‑based in Aspose.Pdf.
                editor.PageRotations = new Dictionary<int, int>
                {
                    { 2, 90 } // key = page number, value = rotation angle
                };

                // Example modification: set zoom to 80% for all pages.
                editor.Zoom = 0.8f;

                // Example modification: change the output page size to A4.
                editor.PageSize = PageSize.A4;

                // Apply all configured changes to the document.
                editor.ApplyChanges();

                // Save the modified PDF.
                // Saving via the facade writes an incremental update, preserving existing signatures.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
