using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath  = "input.xml";
        const string pdfPath  = "temp.pdf";
        const string htmlPath = "preview.html";

        // Verify the source XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // 1. Convert XML to PDF using XmlLoadOptions
            using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
            {
                pdfDoc.Save(pdfPath); // PDF generated from XML
            }

            // 2. Load the generated PDF and convert it to HTML
            using (Document pdfDoc = new Document(pdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Render raster images as PNGs embedded in SVG (cross‑browser friendly)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    pdfDoc.Save(htmlPath, htmlOpts);
                    Console.WriteLine($"HTML preview saved to '{htmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    // GDI+ not available (non‑Windows platforms)
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
                catch (DllNotFoundException)
                {
                    // GDI+ DLL missing
                    Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Optional cleanup of the intermediate PDF
            if (File.Exists(pdfPath))
            {
                try { File.Delete(pdfPath); } catch { }
            }
        }
    }
}