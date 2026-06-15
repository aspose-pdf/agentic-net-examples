using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor and PdfViewer are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_margins.pdf"; // intermediate PDF with margins

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add a 5% margin on all sides of every page.
        //    AddMarginsPct with pages = null processes all pages.
        // ------------------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool success = fileEditor.AddMarginsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,   // null => all pages
            leftMargin:  5,      // 5% left margin
            rightMargin: 5,      // 5% right margin
            topMargin:   5,      // 5% top margin
            bottomMargin:5);     // 5% bottom margin

        if (!success)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
            return;
        }

        // ------------------------------------------------------------
        // 2. Print the resulting PDF using PdfViewer.
        //    AutoResize = true ensures the content fits the printable area.
        // ------------------------------------------------------------
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(outputPath);
            viewer.AutoResize = true;   // scale to fit printable area
            viewer.AutoRotate = true;   // rotate if needed for optimal fit
            viewer.PrintDocument();     // send to default printer
        }
        finally
        {
            viewer.Close(); // release resources
        }

        // Optional: clean up the intermediate file
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // ignore any cleanup errors
        }

        Console.WriteLine("Printing completed.");
    }
}