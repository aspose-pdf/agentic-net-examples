using System;
using System.IO;
using Aspose.Pdf;                 // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)
using Aspose.Pdf.Text;           // Required for any text‑related types (not used here but safe to include)

class Program
{
    static void Main()
    {
        // Input HTML file and desired PDF output file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions tells Aspose.Pdf that the source format is HTML.
            // The Document constructor automatically parses the HTML and builds an internal PDF representation.
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Optional: Optimize resources to reduce size and compress embedded images (including SVG).
                // This step helps to compress any SVG graphics that were converted during the load process.
                doc.OptimizeResources();

                // Save the document as PDF. No SaveOptions are required because the default format is PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}