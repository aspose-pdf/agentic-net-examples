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
            // Load the HTML with rendering forced onto a single PDF page
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                IsRenderToSinglePage = true
            };

            // Document must be wrapped in a using block for deterministic disposal
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                doc.Save(pdfPath); // Saving without options writes PDF by default
            }

            Console.WriteLine($"HTML successfully converted to single‑page PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ library missing
            Console.WriteLine("GDI+ (gdiplus.dll) not found. HTML conversion is unavailable on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}