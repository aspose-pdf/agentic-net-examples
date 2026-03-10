using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output HTML file path
        const string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Create default HTML save options (lifecycle: create)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Save the document as HTML using the options (lifecycle: save)
                // Passing HtmlSaveOptions is required; otherwise Save would produce a PDF regardless of extension.
                pdfDoc.Save(htmlPath, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        // HTML conversion relies on GDI+ and is Windows‑only; handle the platform limitation gracefully.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}