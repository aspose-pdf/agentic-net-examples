using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtmlPath = "input.html";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input HTML file not found: {inputHtmlPath}");
            return;
        }

        // Temporary HTML file that will contain compressed SVG graphics
        string tempCompressedHtml = Path.Combine(Path.GetTempPath(), $"compressed_{Guid.NewGuid()}.html");

        try
        {
            // Load the original HTML document
            using (Document htmlDoc = new Document(inputHtmlPath, new HtmlLoadOptions()))
            {
                // Configure HTML saving to compress any SVG graphics into SVGZ format
                HtmlSaveOptions htmlSaveOpts = new HtmlSaveOptions
                {
                    CompressSvgGraphicsIfAny = true
                };

                // Save the HTML with compressed SVGs to a temporary file
                htmlDoc.Save(tempCompressedHtml, htmlSaveOpts);
            }

            // Load the temporary HTML (now with compressed SVGs) and convert to PDF
            using (Document pdfDoc = new Document(tempCompressedHtml, new HtmlLoadOptions()))
            {
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF with compressed SVG graphics: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempCompressedHtml))
            {
                try { File.Delete(tempCompressedHtml); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}