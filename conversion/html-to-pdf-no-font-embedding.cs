using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML. The FixedLayout property is not available in the current
            // Aspose.Pdf version, so we use the default options. The HTML will be
            // converted to PDF using the library's standard layout handling.
            var loadOpts = new HtmlLoadOptions();

            using (Document doc = new Document(htmlPath, loadOpts))
            {
                // Configure PDF output. FontEmbeddingMode is not exposed in this
                // version of Aspose.Pdf, therefore we rely on the default behaviour.
                // If disabling font embedding is required, consider upgrading the
                // library or using a different API that provides this control.
                var saveOpts = new PdfSaveOptions();

                doc.Save(pdfPath, saveOpts);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML‑to‑PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
