using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "DefaultConversion.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file with default HtmlLoadOptions
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the document as PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Conversion successful: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // GDI+ (required for HTML conversion) is unavailable on non‑Windows platforms
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ DLL missing
            Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}