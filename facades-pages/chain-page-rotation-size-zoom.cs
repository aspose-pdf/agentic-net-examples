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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // ---- Chain page property modifications ----

                // Rotate all pages by 90 degrees (allowed values: 0, 90, 180, 270)
                editor.Rotation = 90;

                // Set the output page size (e.g., A4). PageSize enum is in Aspose.Pdf namespace.
                editor.PageSize = PageSize.A4;

                // Apply a zoom factor; 1.0 = 100%, 0.5 = 50%
                editor.Zoom = 0.5f;

                // Apply the queued changes to the document
                editor.ApplyChanges();

                // Save the modified PDF to the specified file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}