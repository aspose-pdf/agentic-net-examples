using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        // Path to a PDF file that will be printed
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Print a single PDF document using the default printer
        PrintSinglePdf(pdfPath);

        // Print multiple PDF documents in a single print job
        string[] multipleFiles = { "sample.pdf", "another.pdf" };
        PrintMultiplePdfs(multipleFiles);
    }

    // Prints one PDF file using PdfViewer.PrintDocument()
    static void PrintSinglePdf(string filePath)
    {
        // PdfViewer does not implement IDisposable, so use try/finally to ensure Close() is called
        PdfViewer viewer = new PdfViewer();
        try
        {
            // Bind the PDF file to the viewer
            viewer.BindPdf(filePath);

            // Optional printing tweaks
            viewer.AutoResize = true;      // Scale to fit printable area
            viewer.AutoRotate = true;      // Auto‑rotate pages if needed
            viewer.PrintPageDialog = false; // Suppress the page‑range dialog

            // Print using the default system printer
            viewer.PrintDocument();
        }
        finally
        {
            // Release native resources held by the viewer
            viewer.Close();
        }
    }

    // Prints several PDF files using the static PrintDocuments overload that accepts file paths
    static void PrintMultiplePdfs(string[] filePaths)
    {
        // Verify that all files exist before attempting to print
        foreach (var path in filePaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                return;
            }
        }

        // This overload prints all documents in a single print job using the default printer
        PdfViewer.PrintDocuments(filePaths);
    }

    // Example of printing with custom printer and page settings
    static void PrintPdfWithCustomSettings(string filePath)
    {
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(filePath);
            viewer.AutoResize = true;
            viewer.AutoRotate = true;
            viewer.PrintPageDialog = false;

            // Create printer settings (defaults to the system default printer)
            PrinterSettings printerSettings = new PrinterSettings();
            // printerSettings.PrinterName = "MyPrinter"; // Uncomment to target a specific printer

            // Create page settings (e.g., A4 size with no margins)
            PageSettings pageSettings = new PageSettings
            {
                PaperSize = PaperSizes.A4,
                Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0)
            };

            // Print using both page and printer settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }
        finally
        {
            viewer.Close();
        }
    }
}