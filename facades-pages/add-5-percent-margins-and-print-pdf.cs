using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath  = "temp_with_margins.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Add a 5 % margin on all four sides of every page.
        // The AddMarginsPct overload that accepts file paths processes all
        // pages when the pages array is null.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool added = editor.AddMarginsPct(
            source:      inputPath,
            destination: tempPath,
            pages:       null,   // null → all pages
            leftMargin:  5,      // 5 % of page width
            rightMargin: 5,      // 5 % of page width
            topMargin:   5,      // 5 % of page height
            bottomMargin:5);     // 5 % of page height

        if (!added)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Print the modified PDF.
        // AutoResize = true scales the document to fit the printable area,
        // ensuring the added white space is respected.
        // -----------------------------------------------------------------
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(tempPath);
            viewer.AutoResize = true;   // fit to printable area
            viewer.AutoRotate = true;   // rotate if needed
            viewer.PrintDocument();     // print using the default printer
        }
        finally
        {
            viewer.Close(); // release resources
        }

        // Optional: clean up the temporary file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
    }
}