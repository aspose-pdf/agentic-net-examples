using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Configure printer settings for the network printer.
        // Adjust the printer name/URI according to how the printer is installed on the system.
        PrinterSettings printerSettings = new PrinterSettings
        {
            // Example using a UNC share; replace \"Printer\" with the actual share name if needed.
            PrinterName = @"\\192.168.1.100\Printer",
            Copies = 1
        };

        // Load the PDF document (lifecycle rule: use Document constructor and using block).
        using (Document doc = new Document(pdfPath))
        {
            // Initialize PdfViewer with the loaded document.
            using (PdfViewer viewer = new PdfViewer(doc))
            {
                // Suppress any UI dialogs for silent printing.
                viewer.AutoResize = true;      // Fit content to printable area.
                viewer.AutoRotate = true;      // Auto‑rotate pages if needed.
                viewer.PrintPageDialog = false; // No page‑range dialog.

                // Print the document using the specified printer settings.
                viewer.PrintDocumentWithSettings(printerSettings);

                // Close the viewer to release resources.
                viewer.Close();
            }
        }

        Console.WriteLine("Print job dispatched to the network printer.");
    }
}