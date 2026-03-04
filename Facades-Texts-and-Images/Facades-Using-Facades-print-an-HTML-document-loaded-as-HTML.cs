using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";          // HTML source file
        const string printerName = "YourPrinterName"; // optional: specify a printer

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file and convert it to a PDF document in memory
            using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the PDF to a memory stream (no file is created on disk)
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    pdfDoc.Save(pdfStream);
                    pdfStream.Position = 0; // reset for reading

                    // Use the PdfViewer facade to print the PDF stream
                    using (PdfViewer viewer = new PdfViewer())
                    {
                        // Optional: improve printing of large documents
                        viewer.AutoResize = true;
                        viewer.AutoRotate = true;
                        viewer.PrintPageDialog = false;

                        // If you need to target a specific printer, set it here
                        // viewer.PrinterSettings = new Aspose.Pdf.Printing.PrinterSettings { PrinterName = printerName };

                        // Print the PDF stream
                        viewer.PrintLargePdf(pdfStream);
                    }
                }
            }

            Console.WriteLine("HTML document printed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during printing: {ex.Message}");
        }
    }
}