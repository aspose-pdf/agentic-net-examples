using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed raster images as PNG inside SVG to reduce external files
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Convert PDF to HTML
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            // Read the generated HTML
            string htmlContent = File.ReadAllText(outputHtml);

            // Remove HTML comments
            htmlContent = Regex.Replace(htmlContent, @"<!--(.*?)-->", string.Empty, RegexOptions.Singleline);

            // Collapse multiple whitespace characters into a single space
            htmlContent = Regex.Replace(htmlContent, @"\s+", " ");

            // Remove whitespace between tags
            htmlContent = Regex.Replace(htmlContent, @">\s+<", "><");

            // Trim leading/trailing whitespace
            htmlContent = htmlContent.Trim();

            // Write the minified HTML back to the file
            File.WriteAllText(outputHtml, htmlContent);

            Console.WriteLine($"PDF successfully converted and minified HTML saved to '{outputHtml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}