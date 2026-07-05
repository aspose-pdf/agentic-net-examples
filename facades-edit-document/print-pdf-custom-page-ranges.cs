using System;
using System.IO;
using System.Drawing.Printing; // for PrintDocument
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a temporary PDF that contains only pages 1‑5 and 8‑10
        string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        int[] pagesToPrint = { 1, 2, 3, 4, 5, 8, 9, 10 };

        PdfFileEditor editor = new PdfFileEditor();
        bool extracted = editor.Extract(inputPdfPath, pagesToPrint, tempPdfPath);
        if (!extracted)
        {
            Console.Error.WriteLine("Failed to extract the required pages.");
            return;
        }

        // Obtain the default printer name via System.Drawing.Printing.PrintDocument
        using (PrintDocument sysPrintDoc = new PrintDocument())
        {
            // Configure Aspose.Pdf printer settings
            Aspose.Pdf.Printing.PrinterSettings aspPrinterSettings = new Aspose.Pdf.Printing.PrinterSettings
            {
                PrinterName = sysPrintDoc.PrinterSettings.PrinterName,
                PrintRange = Aspose.Pdf.Printing.PrintRange.AllPages // the temp PDF already contains only the desired pages
            };

            // Bind the temporary PDF and print it
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(tempPdfPath);
                viewer.PrintDocumentWithSettings(aspPrinterSettings);
                viewer.Close();
            }
        }

        // Clean up the temporary file
        try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine("Printing completed.");
    }
}