using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define which pages to resize.
        // Use null to resize all pages, or specify 1‑based page numbers (e.g., new int[] {1,3,5})
        int[] pagesToResize = null; // resize all pages

        // New width and height for the page contents (in default space units, i.e., points)
        // Example: 400 points width (~5.55 inches), 600 points height (~8.33 inches)
        double newWidth  = 400.0;
        double newHeight = 600.0;

        // Create the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the resize operation.
        // This method shrinks the page contents to the specified size and adds margins automatically.
        bool success = editor.ResizeContents(
            source:      inputPath,
            destination: outputPath,
            pages:       pagesToResize,
            newWidth:    newWidth,
            newHeight:   newHeight);

        if (success)
        {
            Console.WriteLine($"Pages resized successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize pages.");
        }
    }
}