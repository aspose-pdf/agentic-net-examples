using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;   // required for FormattedText and Position
using Aspose.Pdf.Drawing; // required for Rectangle if needed

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (creation & loading rule)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // -------------------------------------------------
                // Page manipulation using PdfPageEditor (Facade API)
                // -------------------------------------------------
                using (PdfPageEditor pageEditor = new PdfPageEditor())
                {
                    pageEditor.BindPdf(pdfDoc);               // bind the document
                    pageEditor.Rotation = 90;                 // rotate selected pages 90°
                    pageEditor.ProcessPages = new int[] {1}; // apply only to the first page
                    pageEditor.ApplyChanges();                // commit changes
                }

                // -------------------------------------------------
                // Add a header to all pages using PdfFileStamp
                // -------------------------------------------------
                using (PdfFileStamp fileStamp = new PdfFileStamp())
                {
                    fileStamp.BindPdf(pdfDoc); // bind the same document
                    // Create a formatted text object for the header
                    FormattedText headerText = new FormattedText("Sample Header");
                    // Add the header 50 points from the top of each page
                    fileStamp.AddHeader(headerText, 50);
                }

                // -------------------------------------------------
                // Save the modified document as HTML (non‑PDF format)
                // -------------------------------------------------
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Ensure images are embedded as PNGs inside SVG (Windows‑only GDI+ requirement)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Split each PDF page into a separate HTML page (optional)
                    SplitIntoPages = true
                };

                try
                {
                    // Save using explicit HtmlSaveOptions (required rule)
                    pdfDoc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML file created: {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ and is only supported on Windows
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}