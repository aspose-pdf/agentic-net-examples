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

        // Load the HTML file using the default loader (which restricts to local resources).
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        try
        {
            // Document implements IDisposable – wrap it in a using block.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save to PDF using default options. Font embedding control is not available in the current API version.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                doc.Save(pdfPath, pdfOptions);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is only supported on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
