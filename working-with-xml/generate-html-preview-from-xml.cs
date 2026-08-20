using System;
using System.IO;
using Aspose.Pdf;          // Core API (Document, HtmlSaveOptions, XmlLoadOptions)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlInputPath  = "input.xml";   // XML source that generates a PDF
        const string htmlOutputPath = "preview.html";

        // Verify input file exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load the XML and create a PDF document in memory
        using (Document pdfDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // Prepare HTML save options (required to produce HTML, not PDF)
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Example: embed all resources into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Example: save raster images as PNGs embedded in SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the document as HTML for web preview
            pdfDoc.Save(htmlOutputPath, htmlOpts);
        }

        Console.WriteLine($"HTML preview generated at '{htmlOutputPath}'.");
    }
}