using System;
using System.IO;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "selected_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract required pages (1‑5 and 8‑10) into a temporary PDF
        PdfFileEditor fileEditor = new PdfFileEditor();
        int[] pageNumbers = new int[] { 1, 2, 3, 4, 5, 8, 9, 10 };
        bool extracted = fileEditor.Extract(inputPath, pageNumbers, tempPath);
        if (!extracted)
        {
            Console.Error.WriteLine("Failed to extract pages.");
            return;
        }

        // Prepare printer settings using System.Drawing.Printing.PrintDocument to obtain default printer name
        System.Drawing.Printing.PrintDocument sysPrintDoc = new System.Drawing.Printing.PrintDocument();
        Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings();
        printerSettings.PrinterName = sysPrintDoc.PrinterSettings.PrinterName;
        printerSettings.PrintRange = Aspose.Pdf.Printing.PrintRange.AllPages;

        // Page settings (A4 paper, zero margins)
        Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings();
        pageSettings.PaperSize = Aspose.Pdf.Printing.PaperSizes.A4;
        pageSettings.Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0);

        // Print the temporary PDF containing the selected pages
        PdfViewer.PrintDocuments(printerSettings, pageSettings, tempPath);

        // Clean up temporary file
        try
        {
            File.Delete(tempPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}