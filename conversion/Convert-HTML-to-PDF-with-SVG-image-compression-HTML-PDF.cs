using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (HtmlLoadOptions resides here)

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
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
                // NOTE:
                // HtmlSaveOptions.CompressSvgGraphicsIfAny is relevant when saving *to* HTML.
                // When converting HTML → PDF there is no direct option to compress SVG
                // graphics; Aspose.Pdf automatically embeds SVG content as vector graphics
                // in the PDF. No additional code is required for compression.

                // Save the loaded document as PDF
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