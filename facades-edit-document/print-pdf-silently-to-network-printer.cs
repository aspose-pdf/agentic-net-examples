using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // Path to the PDF to print
        const string printerName = "192.168.1.100";        // Network printer identifier

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize the PdfViewer facade and bind the PDF file
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(pdfPath);
                viewer.AutoResize = true;      // Adjust size to printable area
                viewer.AutoRotate = true;      // Auto‑rotate pages if needed
                viewer.PrintPageDialog = false; // Suppress any print dialog (silent)

                // Configure printer settings to target the network printer
                PrinterSettings printerSettings = new PrinterSettings
                {
                    PrinterName = printerName
                };

                // Print the document silently using the specified printer
                viewer.PrintDocumentWithSettings(printerSettings);
                viewer.Close(); // Explicit close (optional, Dispose will also handle it)
            }

            Console.WriteLine("Print job sent successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
    }
}