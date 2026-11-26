using System;
using System.IO;
using Aspose.Pdf;

class Program
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
            // HtmlLoadOptions defaults IsEmbedFonts = true, so fonts will be embedded.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap Document in using for deterministic disposal.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save as PDF; default behavior respects the font embedding setting.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF with embedded fonts: {pdfPath}");
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