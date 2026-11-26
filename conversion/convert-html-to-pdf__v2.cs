using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "Sample.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. No explicit HtmlLoadOptions are required for basic conversion.
            using (Document doc = new Document(htmlPath))
            {
                // Save the document as PDF. No SaveOptions needed because the target format is PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML-to-PDF conversion relies on GDI+ and is only supported on Windows.
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ native library missing.
            Console.WriteLine("GDI+ library not found. HTML to PDF conversion is unavailable on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}