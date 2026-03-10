using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to print.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfViewer implements IDisposable, so wrap it in a using block.
        using (PdfViewer viewer = new PdfViewer())
        {
            // Bind the PDF file to the viewer. No explicit BindPdf(string) overload is needed beyond this call.
            viewer.BindPdf(pdfPath);

            // Preserve layout and page‑size fidelity:
            viewer.AutoResize = true;   // Scale the document to fit the printable area.
            viewer.AutoRotate = true;   // Auto‑rotate pages when the orientation differs.
            viewer.PrintPageDialog = false; // Suppress the page‑range dialog.

            // Configure printer settings. If you need a specific printer, set its name here.
            PrinterSettings printerSettings = new PrinterSettings();
            // printerSettings.PrinterName = "YourPrinterName";

            // Configure page settings to match the target paper size.
            // Here we use A4 as an example; you can retrieve the original page size from the PDF if required.
            PageSettings pageSettings = new PageSettings
            {
                PaperSize = new PaperSize("A4", 827, 1169), // Width and height in hundredths of an inch.
                Margins = new Margins(0, 0, 0, 0)           // No additional margins.
            };

            // Print the document using the specified printer and page settings.
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}