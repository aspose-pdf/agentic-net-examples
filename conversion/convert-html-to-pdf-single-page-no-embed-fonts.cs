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

        // Configure HTML loading options
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            IsEmbedFonts        = false, // do not embed fonts
            IsRenderToSinglePage = true   // render the whole document on a single PDF page
        };

        try
        {
            // Load the HTML file with the specified options
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as PDF (default format)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ library missing on non‑Windows platforms
            Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}