using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string htmlPath   = "input.html";
        const string tempPdfPath = "temp_from_html.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // 1. Load the HTML file and convert it to a PDF document.
        //    HtmlLoadOptions is used to control the conversion.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Optional: save the intermediate PDF (useful for debugging)
            pdfDoc.Save(tempPdfPath);

            // 2. Use the PdfViewer facade to render and print the PDF.
            //    PdfViewer implements IFacade (IDisposable), so we can use a using block.
            using (PdfViewer viewer = new PdfViewer())
            {
                // Bind the PDF document to the viewer.
                viewer.BindPdf(pdfDoc);

                // Configure printing options (optional).
                viewer.AutoResize = true;          // Fit to printable area
                viewer.AutoRotate = true;          // Auto‑rotate pages if needed
                viewer.PrintPageDialog = false;    // Suppress page‑number dialog

                // Render and send the document to the default printer.
                viewer.PrintDocument();

                // The viewer is automatically closed/disposed at the end of the using block.
            }
        }

        // Clean up the temporary PDF file if desired.
        try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine("HTML document rendered and sent to printer successfully.");
    }
}