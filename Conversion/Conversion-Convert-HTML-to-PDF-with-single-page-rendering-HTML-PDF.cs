using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load HTML with single‑page rendering enabled
            var loadOptions = new HtmlLoadOptions
            {
                IsRenderToSinglePage = true
            };

            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Save as PDF (extension determines format)
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ (Windows only)
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ library missing
            Console.WriteLine("GDI+ (gdiplus.dll) not found. HTML to PDF conversion is unavailable.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}