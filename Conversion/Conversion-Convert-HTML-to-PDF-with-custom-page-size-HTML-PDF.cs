using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, PageInfo)
using Aspose.Pdf.Text;          // Required for text‑related types (not used here but safe to include)

class HtmlToPdfConverter
{
    static void Main()
    {
        const string htmlPath   = "input.html";
        const string pdfPath    = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file. HtmlLoadOptions is in Aspose.Pdf namespace.
            // This operation requires GDI+; on non‑Windows platforms it may throw TypeInitializationException.
            HtmlLoadOptions loadOpts = new HtmlLoadOptions();

            using (Document doc = new Document(htmlPath, loadOpts))   // Document is wrapped in using (lifecycle rule)
            {
                // Set a custom page size (e.g., 8.5in x 11in = 612pt x 792pt).
                // PageInfo can be assigned to the whole document before saving.
                doc.PageInfo = new PageInfo
                {
                    Width  = 612,   // 8.5 inches * 72 points per inch
                    Height = 792    // 11 inches * 72 points per inch
                };

                // Save as PDF. No SaveOptions needed because the target format is PDF.
                doc.Save(pdfPath);   // Save() without SaveOptions writes PDF (save-to-non-pdf rule)
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion requires GDI+ (Windows only).
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}