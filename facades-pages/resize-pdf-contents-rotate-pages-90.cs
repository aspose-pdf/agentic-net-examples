using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string outputPath  = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Step 1: Resize page contents (no size change in this example, just demonstrates the API)
        PdfFileEditor resizer = new PdfFileEditor();
        // newWidth and newHeight are specified in default space units; 100 means 100 points.
        // Using the same values keeps the original size while still invoking the resize operation.
        resizer.ResizeContents(inputPath, resizedPath, null, 100, 100);
        // No explicit Close needed for PdfFileEditor; it does not implement IDisposable.

        // Step 2: Rotate the resized PDF by 90 degrees using PdfPageEditor (facade API)
        using (Document doc = new Document(resizedPath))
        {
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);               // Initialize the facade with the document
            editor.Rotation = 90;              // Rotate all pages by 90 degrees (must be 0,90,180,270)
            editor.ApplyChanges();             // Apply the rotation

            // Save the final result
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page rotated and saved to '{outputPath}'.");
    }
}