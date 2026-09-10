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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfPageEditor (also disposable)
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // ---- Chain of page modifications ----

                // 1. Rotate all pages by 90 degrees (allowed values: 0, 90, 180, 270)
                editor.Rotation = 90;

                // 2. Set zoom factor (1.0 = 100%). Here we use 75% zoom.
                editor.Zoom = 0.75f;

                // 3. Change the output page size to A4.
                //    PageSize enum is defined in Aspose.Pdf namespace.
                editor.PageSize = PageSize.A4;

                // 4. Optionally move the origin of the original content.
                //    This shifts the content 50 points to the right and 30 points up.
                editor.MovePosition(50, 30);

                // Apply all the above changes to the bound document.
                editor.ApplyChanges();

                // Save the modified PDF to the specified output file.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}