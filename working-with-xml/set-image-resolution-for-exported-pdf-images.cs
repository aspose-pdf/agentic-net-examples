using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputHtml = "output.html";
        const int desiredDpi = 300; // Image resolution in DPI

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML source (e.g., XSL‑FO) using the appropriate load options.
        using (Document doc = new Document(xmlPath, new XslFoLoadOptions()))
        {
            // Configure HTML export options and set the image resolution.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                ImageResolution = desiredDpi,
                // Optional: embed all resources into the HTML for a single file output.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Export the PDF (generated from XML) to HTML with the specified image DPI.
            doc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"Export completed. Images rendered at {desiredDpi} DPI.");
    }
}