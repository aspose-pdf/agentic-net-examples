using System;
using System.IO;
using Aspose.Pdf;   // Aspose.Pdf contains Document, HtmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Directory that contains the source HTML file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML and output PDF file paths.
        string htmlPath = Path.Combine(dataDir, "input.html");
        string pdfPath  = Path.Combine(dataDir, "output.pdf");

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file. HtmlLoadOptions can be customized if needed.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        try
        {
            // Wrap the Document in a using block for deterministic disposal.
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF. No SaveOptions are required for PDF output.
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}