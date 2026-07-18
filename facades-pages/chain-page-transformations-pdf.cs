using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Facades;      // Facade classes (PdfPageEditor)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialise PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // ---- Chain of page modifications ----

                // 1. Rotate all pages by 90 degrees (allowed values: 0, 90, 180, 270)
                editor.Rotation = 90;

                // 2. Set zoom factor (1.0 = 100%). Here we use 0.5 = 50%
                editor.Zoom = 0.5f;

                // 3. Change the output page size. Example: A4 size
                editor.PageSize = PageSize.A4;

                // Optional: shift the origin of the page content (x, y) in points
                editor.MovePosition(50f, 100f);

                // Apply the accumulated changes to the document
                editor.ApplyChanges();

                // Save the modified PDF to the specified output file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}