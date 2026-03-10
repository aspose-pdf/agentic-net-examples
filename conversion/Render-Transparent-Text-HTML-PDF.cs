using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input HTML file and desired PDF output file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions can be customized if needed
            // (e.g., BasePath, PageSize, etc.). Here we use the default options.
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // No need to flatten transparency – we want to keep any transparent
                // content as‑is. The default PDF save will preserve text as selectable.
                doc.Save(pdfPath);   // Saves as PDF (default format)
            }

            Console.WriteLine($"HTML successfully rendered to PDF: '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}