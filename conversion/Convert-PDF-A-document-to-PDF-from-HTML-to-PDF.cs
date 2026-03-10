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
        const string htmlInputPath  = "input.html";   // source HTML file
        const string pdfOutputPath  = "output_from_html.pdf";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML source not found: {htmlInputPath}");
        }
        else
        {
            try
            {
                // Load the HTML using HtmlLoadOptions (required for HTML → PDF conversion)
                HtmlLoadOptions htmlLoadOptions = new HtmlLoadOptions();

                // Document constructor loads the HTML and creates a PDF in memory
                using (Document htmlDoc = new Document(htmlInputPath, htmlLoadOptions))
                {
                    // Save without explicit SaveOptions – this always writes a PDF
                    htmlDoc.Save(pdfOutputPath);
                }

                Console.WriteLine($"HTML successfully converted to PDF: '{pdfOutputPath}'");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and fails on non‑Windows platforms
                Console.WriteLine("HTML → PDF conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML → PDF conversion: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // 2. Convert a PDF/A document to a regular PDF (non‑PDF/A) document.
        // -----------------------------------------------------------------
        const string pdfaInputPath = "input_pdfa.pdf";   // source PDF/A file
        const string pdfPlainPath = "output_from_pdfa.pdf";

        if (!File.Exists(pdfaInputPath))
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaInputPath}");
        }
        else
        {
            try
            {
                // Load the PDF/A document. No special LoadOptions are required.
                using (Document pdfaDoc = new Document(pdfaInputPath))
                {
                    // Saving with the default Save() writes a regular PDF.
                    // The PDF/A compliance flags are not preserved unless explicitly set.
                    pdfaDoc.Save(pdfPlainPath);
                }

                Console.WriteLine($"PDF/A successfully converted to regular PDF: '{pdfPlainPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF/A → PDF conversion: {ex.Message}");
            }
        }
    }
}