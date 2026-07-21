using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Resize contents of all pages to 95% of original width and height.
        // This uniformly shrinks page elements by 5% and adds equal margins.
        bool success = fileEditor.ResizeContentsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,      // null processes all pages
            newWidth:    95,        // new content width as percent of original
            newHeight:   95);       // new content height as percent of original

        if (success)
        {
            Console.WriteLine($"Pages resized successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF pages.");
        }
    }
}