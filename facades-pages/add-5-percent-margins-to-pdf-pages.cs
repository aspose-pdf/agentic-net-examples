using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "input_with_margins.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Add a 5% margin on all sides of every page
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool added = fileEditor.AddMarginsPct(
            inputPath,               // source PDF
            outputPath,              // destination PDF
            null,                    // null = all pages
            5,   // left margin 5%
            5,   // right margin 5%
            5,   // top margin 5%
            5);  // bottom margin 5%

        if (!added)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
            return;
        }

        // Print the PDF with the added margins
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(outputPath);
            viewer.AutoResize = true; // fit content to printable area
            viewer.PrintDocument();   // print using default printer
            viewer.Close();
        }

        // Optional cleanup of the temporary file
        try { File.Delete(outputPath); } catch { }
    }
}