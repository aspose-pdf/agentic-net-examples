using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Convert an HTML file to a regular PDF document.
        // -----------------------------------------------------------------
        const string htmlInputPath  = "input.html";
        const string pdfFromHtmlPath = "converted_from_html.pdf";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML source not found: {htmlInputPath}");
        }
        else
        {
            try
            {
                // HtmlLoadOptions is required for loading HTML. It lives in Aspose.Pdf.
                using (Document htmlDoc = new Document(htmlInputPath, new HtmlLoadOptions()))
                {
                    // Saving without explicit SaveOptions always produces a PDF.
                    htmlDoc.Save(pdfFromHtmlPath);
                }

                Console.WriteLine($"HTML → PDF conversion succeeded: {pdfFromHtmlPath}");
            }
            catch (TypeInitializationException)
            {
                // HTML → PDF conversion uses GDI+ and is Windows‑only.
                Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML → PDF conversion: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // 2. Convert a PDF/A document to a regular PDF (remove PDF/A compliance).
        // -----------------------------------------------------------------
        const string pdfaInputPath = "input_pdfa.pdf";
        const string pdfOutputPath = "converted_from_pdfa.pdf";

        if (!File.Exists(pdfaInputPath))
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaInputPath}");
        }
        else
        {
            try
            {
                using (Document pdfaDoc = new Document(pdfaInputPath))
                {
                    // Remove PDF/A compliance flags; the document becomes a normal PDF.
                    pdfaDoc.RemovePdfaCompliance();

                    // Save the resulting PDF.
                    pdfaDoc.Save(pdfOutputPath);
                }

                Console.WriteLine($"PDF/A → PDF conversion succeeded: {pdfOutputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF/A → PDF conversion: {ex.Message}");
            }
        }
    }
}