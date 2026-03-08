using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "resized_output.pdf";

        // Desired page size in default PDF units (points). 
        // 1 point = 1/72 inch. Adjust as needed.
        const double newWidth  = 595; // e.g., A4 width 8.27in * 72 = 595 points
        const double newHeight = 842; // e.g., A4 height 11.69in * 72 = 842 points

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Resize all pages (pages == null) to the specified dimensions.
        // This overload shrinks the existing page contents to fit the new size
        // and adds blank margins where necessary, preserving the original content.
        bool success = fileEditor.ResizeContents(
            source:      inputPath,
            destination: outputPath,
            pages:       null,        // null = all pages
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