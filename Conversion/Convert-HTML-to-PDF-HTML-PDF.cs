using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class HtmlToPdfConverter
{
    static void Main()
    {
        // Paths – adjust as needed.
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string htmlPath        = Path.Combine(dataDir, "HTML-to-PDF.html");
        string pdfPath         = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Verify the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file using HtmlLoadOptions (empty base path).
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Document constructor with (string filePath, LoadOptions) loads the HTML and creates a PDF in memory.
            using (Document pdfDocument = new Document(htmlPath, loadOptions))
            {
                // Save the in‑memory PDF to the desired output file.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        // HTML‑to‑PDF conversion relies on GDI+; on non‑Windows platforms this may throw TypeInitializationException.
        catch (TypeInitializationException)
        {
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}