using System;
using System.IO;
using System.Drawing.Printing;                     // PrintDocument and System printer settings
using Aspose.Pdf;                                 // Document (if needed)
using Aspose.Pdf.Facades;                         // PdfViewer, PdfFileEditor
using Aspose.Pdf.Printing;                        // Aspose printer settings (qualified to avoid ambiguity)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string tempPdfPath = "temp_selected_pages.pdf"; // intermediate PDF

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Extract the required pages (1‑5 and 8‑10) into a temporary PDF.
        // PdfFileEditor.Extract accepts an int[] with the exact page numbers.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        int[] pagesToPrint = new int[] { 1, 2, 3, 4, 5, 8, 9, 10 };
        bool extracted = editor.Extract(inputPdfPath, pagesToPrint, tempPdfPath);
        if (!extracted)
        {
            Console.Error.WriteLine("Failed to extract the selected pages.");
            return;
        }

        // ---------------------------------------------------------------
        // Prepare printer settings using the system's default printer.
        // Use Aspose.Pdf.Printing.PrinterSettings (not System.Drawing one).
        // ---------------------------------------------------------------
        PrintDocument sysPrintDoc = new PrintDocument();
        // Create Aspose printer settings and copy the printer name from the system settings.
        Aspose.Pdf.Printing.PrinterSettings aspPrinterSettings = new Aspose.Pdf.Printing.PrinterSettings
        {
            PrinterName = sysPrintDoc.PrinterSettings.PrinterName,
            // Optional: you can also copy other properties if needed, e.g., Copies, Collate, etc.
        };

        // ---------------------------------------------------------------
        // Use PdfViewer to print the temporary PDF.
        // BindPdf loads the document; PrintDocumentWithSettings prints it.
        // ---------------------------------------------------------------
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(tempPdfPath);
            // Optional: suppress the page range dialog
            viewer.PrintPageDialog = false;
            // Print using the prepared Aspose printer settings
            viewer.PrintDocumentWithSettings(aspPrinterSettings);
        }

        // Clean up the temporary file
        try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine("Printing completed.");
    }
}
