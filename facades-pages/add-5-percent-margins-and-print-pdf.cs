using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp_with_margins.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Apply a 5% margin on all sides of every page
        PdfFileEditor editor = new PdfFileEditor();
        bool added = editor.AddMarginsPct(
            inputPath,          // source PDF
            tempPath,           // destination PDF with margins
            null,               // null = all pages
            5,                  // left margin percent
            5,                  // right margin percent
            5,                  // top margin percent
            5);                 // bottom margin percent

        if (!added)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
            return;
        }

        // Print the resized PDF; AutoResize ensures it fits the printable area
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(tempPath);
            viewer.AutoResize = true;
            viewer.PrintDocument();
            viewer.Close();
        }

        // Clean up temporary file (optional)
        try { File.Delete(tempPath); } catch { }
    }
}