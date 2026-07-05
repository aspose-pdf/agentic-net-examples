using System;
using System.IO;
using System.Drawing.Printing;               // For obtaining the default system printer name
using Aspose.Pdf;                           // Core PDF types
using Aspose.Pdf.Facades;                   // PdfViewer facade
using Aspose.Pdf.Printing;                  // PrinterSettings and Duplex enums

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify that the PDF file exists before attempting to print
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Configure printer settings:
        //   • Use the default system printer.
        //   • Enable collated copies.
        //   • Set the number of copies (example: 2).
        //   • Enable duplex (double‑sided) printing – vertical flip.
        // ------------------------------------------------------------
        Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings();

        // Obtain the default printer name from the .NET PrintDocument class
        PrintDocument sysPrintDoc = new PrintDocument();
        printerSettings.PrinterName = sysPrintDoc.PrinterSettings.PrinterName;

        printerSettings.Collate = true;                     // Collated copies
        printerSettings.Copies = 2;                         // Number of copies (adjust as needed)
        printerSettings.Duplex = Aspose.Pdf.Printing.Duplex.Vertical; // Double‑sided (short‑edge flip)

        // ------------------------------------------------------------
        // Print the PDF using PdfViewer with the configured settings.
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.PdfViewer viewer = new Aspose.Pdf.Facades.PdfViewer();
        try
        {
            viewer.BindPdf(pdfPath);                         // Load the PDF into the viewer
            viewer.PrintDocumentWithSettings(printerSettings); // Print with duplex & collated copies
        }
        finally
        {
            viewer.Close();                                 // Release resources
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}