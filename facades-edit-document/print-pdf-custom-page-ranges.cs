using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;
using System.Drawing.Printing;

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

        // Initialize PdfViewer and bind the PDF file
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Obtain the default printer name from System.Drawing.Printing.PrintDocument
            PrintDocument sysPrintDoc = new PrintDocument();

            // Configure Aspose printer settings
            Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings
            {
                PrinterName = sysPrintDoc.PrinterSettings.PrinterName,
                PrintRange = Aspose.Pdf.Printing.PrintRange.SomePages
            };

            // Configure page settings (optional: A4 size, zero margins)
            Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings
            {
                PaperSize = Aspose.Pdf.Printing.PaperSizes.A4,
                Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0)
            };

            // First range: pages 1‑5
            printerSettings.FromPage = 1;
            printerSettings.ToPage = 5;
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // Second range: pages 8‑10
            printerSettings.FromPage = 8;
            printerSettings.ToPage = 10;
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            viewer.Close();
        }

        Console.WriteLine("Print jobs for ranges 1‑5 and 8‑10 have been submitted.");
    }
}