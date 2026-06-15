using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Text;          // For Rotation enum (if needed)

class RotatePageExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Store the original rotation value
            Rotation originalRotation = page.Rotate;

            // Example: apply a rotation (90° clockwise)
            page.Rotate = Rotation.on90;

            // ... perform any operations while the page is rotated ...

            // Restore the page to its original orientation
            page.Rotate = originalRotation;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page rotation restored and saved to '{outputPath}'.");
    }
}