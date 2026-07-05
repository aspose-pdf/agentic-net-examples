using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Text;          // For any text-related types (not needed here but safe)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath  = "input_from_xml.pdf";   // PDF generated from XML
        const string htmlPath = "preview.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Initialize HTML save options (required for HTML output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example: embed raster images as PNG inside SVG (Windows‑only GDI+ requirement)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML – passing HtmlSaveOptions ensures HTML output
                pdfDoc.Save(htmlPath, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
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