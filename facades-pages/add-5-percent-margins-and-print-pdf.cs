using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_margins.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Add a 5% margin on all sides of every page
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool added = fileEditor.AddMarginsPct(
            inputPath,               // source PDF
            outputPath,              // destination PDF
            null,                    // null = all pages
            5,                       // left margin (%)
            5,                       // right margin (%)
            5,                       // top margin (%)
            5);                      // bottom margin (%)

        if (!added)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
            return;
        }

        // Print the resulting PDF with automatic resizing to fit the printable area
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(outputPath);
            viewer.AutoResize = true;   // ensure content fits the page when printing
            viewer.PrintDocument();     // print using the default printer
            viewer.Close();             // close the viewer (optional, disposed by using)
        }

        // Clean up temporary file if desired
        try { File.Delete(outputPath); } catch { }
    }
}