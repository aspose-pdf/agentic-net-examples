using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // path to the source PDF
        const string outputPath = "edited.pdf";  // path where the edited PDF will be saved

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap it in a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF file for editing
            editor.BindPdf(inputPath);

            // Example preparation: set rotation, zoom, or page size if needed
            // editor.Rotation = 90;          // rotate all pages 90 degrees
            // editor.Zoom = 1.5;             // increase page content size by 150%
            // editor.PageSize = new Aspose.Pdf.PageSize(595, 842); // A4 size

            // Apply the configured changes to the document
            editor.ApplyChanges();

            // Save the edited PDF to the desired output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF loaded, edited, and saved to '{outputPath}'.");
    }
}