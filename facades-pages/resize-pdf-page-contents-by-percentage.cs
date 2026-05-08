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
        // This effectively adds a 2.5% margin on each side (negative margin of 5% total).
        bool resized = fileEditor.ResizeContentsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,    // null processes all pages
            newWidth:    95,      // new content width in percent
            newHeight:   95);     // new content height in percent

        // Report the result
        if (resized)
            Console.WriteLine($"Successfully resized contents. Output saved to '{outputPath}'.");
        else
            Console.WriteLine("Resize operation failed.");
    }
}