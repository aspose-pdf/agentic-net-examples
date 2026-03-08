using System;
using Aspose.Pdf.Facades;   // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath  = "input.pdf";
        // Output PDF file path (resized version)
        const string outputPath = "resized_output.pdf";

        // Desired page size in default PDF units (points; 1 inch = 72 points)
        // Example: A4 size = 595 x 842 points
        double newWidth  = 595; // width in points
        double newHeight = 842; // height in points

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor provides the ResizeContents method (Facades API)
        // The overload with (string source, string destination, int[] pages, double newWidth, double newHeight)
        // resizes the contents of the specified pages. Passing null for 'pages' processes all pages.
        PdfFileEditor fileEditor = new PdfFileEditor();

        bool success = fileEditor.ResizeContents(
            source:      inputPath,
            destination: outputPath,
            pages:       null,        // null => all pages
            newWidth:    newWidth,
            newHeight:   newHeight);

        if (success)
        {
            Console.WriteLine($"PDF pages resized successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF pages.");
        }
    }
}