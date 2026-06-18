using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath   = "input.xml";      // XML source that will be turned into PDF/HTML
        const string htmlPath  = "preview.html";   // Desired HTML output for web preview

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found – {xmlPath}");
            return;
        }

        try
        {
            // Load the XML and create a PDF document in memory.
            // XmlLoadOptions is required for loading XML files.
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Prepare HTML save options – explicit options are mandatory for HTML output.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Example: embed all resources into a single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Use PNG images embedded into SVG (default, can be changed if needed).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML. This conversion may require GDI+ (Windows only).
                // Wrap in try‑catch to handle possible TypeInitializationException on non‑Windows platforms.
                try
                {
                    doc.Save(htmlPath, htmlOptions);
                    Console.WriteLine($"HTML preview generated successfully: {htmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.Error.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}