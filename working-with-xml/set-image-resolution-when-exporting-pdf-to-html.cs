using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string htmlPath = "output.html";
        const int desiredDpi = 300; // Image resolution in DPI

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML source into a PDF document
        using (Document doc = new Document())
        {
            doc.BindXml(xmlPath); // Load XML content (no XmlLoadOptions needed)

            // Configure HTML export options with the required image resolution
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                ImageResolution = desiredDpi,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Export the PDF pages as HTML, applying the image resolution setting
            doc.Save(htmlPath, htmlOpts);
        }

        Console.WriteLine($"HTML exported to '{htmlPath}' with image resolution set to {desiredDpi} DPI.");
    }
}