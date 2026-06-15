using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";   // source XML file
        const string htmlPath  = "output.html"; // exported HTML with images
        const int    resolutionDpi = 300;        // desired image resolution

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Source file not found: {xmlPath}");
            return;
        }

        // Load XML content into a PDF document.
        // Use BindXml (not the Document constructor) as required for XML sources.
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(xmlPath);

            // Configure HTML export options.
            // ImageResolution controls the DPI of raster images generated during the conversion.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                ImageResolution = resolutionDpi,
                // Optional: embed all resources into a single HTML file.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Optional: embed raster images as PNG inside SVG (cross‑platform friendly).
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Export the PDF (generated from XML) to HTML with the specified image resolution.
            pdfDoc.Save(htmlPath, htmlOpts);
        }

        Console.WriteLine($"XML converted to HTML with images at {resolutionDpi} DPI: {htmlPath}");
    }
}