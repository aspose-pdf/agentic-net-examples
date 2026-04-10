using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing; // Added for Aspose printer settings

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_margin.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Add a 5% margin on all sides of every page
        PdfFileEditor fileEditor = new PdfFileEditor();
        using (FileStream src = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream dest = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // null pages array processes all pages
            bool success = fileEditor.AddMarginsPct(src, dest, null, 5, 5, 5, 5);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add margins to the PDF.");
                return;
            }
        }

        // Print the resized PDF with auto‑scaling to fit the printable area
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(outputPath);
            viewer.AutoResize = true; // ensure content fits the page when printing

            // Use Aspose.Pdf.Printing.PrinterSettings (not System.Drawing.Printing.PrinterSettings)
            PrinterSettings printerSettings = new PrinterSettings(); // default printer settings for Aspose
            // Additional printer settings can be configured here if needed, e.g.:
            // printerSettings.Copies = 1;
            // printerSettings.PrinterName = "YourPrinterName";

            viewer.PrintDocumentWithSettings(printerSettings);
        }
        finally
        {
            viewer.Close();
        }

        Console.WriteLine($"Successfully printed PDF with 5% margins: {outputPath}");
    }
}
