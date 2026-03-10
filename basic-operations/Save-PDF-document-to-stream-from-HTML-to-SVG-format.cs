using System;
using System.IO;
using Aspose.Pdf;               // Document, HtmlLoadOptions, SvgSaveOptions
using Aspose.Pdf.Text;          // required for HTML conversion (GDI+)

class HtmlToSvgConverter
{
    /// <summary>
    /// Converts an HTML file to SVG and writes the result into a memory stream.
    /// The stream contains the SVG data of the first page (or all pages if the source HTML has multiple pages).
    /// </summary>
    /// <param name="htmlPath">Full path to the source HTML file.</param>
    /// <returns>A MemoryStream positioned at the beginning, containing the SVG output.</returns>
    public static MemoryStream ConvertHtmlToSvg(string htmlPath)
    {
        if (!File.Exists(htmlPath))
            throw new FileNotFoundException($"HTML file not found: {htmlPath}");

        // Load the HTML document. HTML conversion requires GDI+ (Windows only),
        // so we wrap it in a try‑catch to handle possible TypeInitializationException on other platforms.
        try
        {
            // LoadOptions for HTML are HtmlLoadOptions (no separate namespace needed).
            using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Prepare SVG save options – required when saving to a non‑PDF format.
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the PDF (generated from HTML) to a memory stream as SVG.
                // The using statement ensures the stream is disposed by the caller.
                MemoryStream svgStream = new MemoryStream();
                pdfDoc.Save(svgStream, svgOptions);

                // Reset the stream position so it can be read from the beginning.
                svgStream.Position = 0;
                return svgStream; // Caller is responsible for disposing the returned stream.
            }
        }
        catch (TypeInitializationException tie)
        {
            // GDI+ is unavailable (e.g., on macOS/Linux). Re‑throw with a clearer message.
            throw new PlatformNotSupportedException(
                "HTML to SVG conversion requires GDI+ and is only supported on Windows.", tie);
        }
    }

    // Example usage
    static void Main()
    {
        const string htmlFile = "sample.html";

        try
        {
            using (MemoryStream svgResult = ConvertHtmlToSvg(htmlFile))
            {
                // For demonstration, write the SVG to a file.
                const string outputSvg = "output.svg";
                using (FileStream file = new FileStream(outputSvg, FileMode.Create, FileAccess.Write))
                {
                    svgResult.CopyTo(file);
                }

                Console.WriteLine($"HTML converted to SVG and saved as '{outputSvg}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}