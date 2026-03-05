using System;
using System.IO;
using Aspose.Pdf;

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
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize HTML save options (single‑page output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Ensure all pages are rendered into one HTML file
                    SplitIntoPages = false
                };

                // Save the document as HTML (lifecycle: save)
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}