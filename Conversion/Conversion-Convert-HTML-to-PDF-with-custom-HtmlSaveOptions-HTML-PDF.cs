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
            // Load the HTML file. HtmlLoadOptions can be customized if needed.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            // Example: set a base path for relative resources
            // loadOptions.BasePath = Path.GetDirectoryName(Path.GetFullPath(htmlPath));

            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF. No SaveOptions are required for PDF output.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+ and works only on Windows.
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}