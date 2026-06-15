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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true
                };

                // Save the document as HTML using the configured options
                // On non‑Windows platforms this may throw TypeInitializationException (GDI+ requirement)
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to multi‑page HTML: {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}