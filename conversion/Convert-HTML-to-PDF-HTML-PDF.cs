using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input HTML and output PDF file paths.
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");
        string pdfPath  = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load options for converting HTML to PDF.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        try
        {
            // Load the HTML document and convert it to a PDF.
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}