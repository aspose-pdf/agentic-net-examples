using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Required for TextFragment (used only on Windows)

class Program
{
    static void Main()
    {
        // Input and output PDF file paths (relative to the executable directory)
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Desired page dimensions in points (1 point = 1/72 inch)
        // Example: 5.5 x 8.5 inches => 396 x 612 points
        double newWidth  = 396; // width in points
        double newHeight = 612; // height in points

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple placeholder
        // document **only on Windows** because placeholder creation uses System.Drawing
        // (via TextFragment) which requires GDI+.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a one‑page PDF with a short text paragraph.
                var placeholderDoc = new Document();
                var page = placeholderDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Placeholder PDF – generated because 'input.pdf' was not found."));
                placeholderDoc.Save(inputPath);
                Console.WriteLine($"Created placeholder PDF at '{Path.GetFullPath(inputPath)}'.");
            }
            else
            {
                Console.WriteLine("Input PDF not found and placeholder generation requires GDI+ which is unavailable on this platform. Please provide an existing PDF file.");
                return;
            }
        }

        try
        {
            // Create the facade that provides page‑editing capabilities.
            var editor = new PdfFileEditor();

            // Resize the contents of all pages.
            // Passing null for the pages array means “apply to every page".
            // The method shrinks the page contents to the specified size and adds
            // blank margins around them, preserving the original layout.
            editor.ResizeContents(inputPath, outputPath, null, newWidth, newHeight);

            Console.WriteLine($"Resized PDF saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while resizing the PDF: {ex.Message}");
        }
    }
}
