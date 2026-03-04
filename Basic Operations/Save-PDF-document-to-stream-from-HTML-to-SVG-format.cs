using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";

        // Validate input file existence
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Stream that will receive the SVG output
        using (MemoryStream svgStream = new MemoryStream())
        {
            try
            {
                // Load the HTML document – HtmlLoadOptions is required for HTML input
                using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
                {
                    // Initialize SVG save options (required for non‑PDF output)
                    SvgSaveOptions svgOptions = new SvgSaveOptions();

                    // Save the document as SVG into the memory stream
                    pdfDoc.Save(svgStream, svgOptions);
                }

                // Reset stream position for further processing (e.g., write to file, send over network)
                svgStream.Position = 0;

                // Example: write the SVG data to a file for verification
                const string outputSvgPath = "output.svg";
                using (FileStream file = new FileStream(outputSvgPath, FileMode.Create, FileAccess.Write))
                {
                    svgStream.CopyTo(file);
                }

                Console.WriteLine($"SVG saved to stream and written to '{outputSvgPath}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML → SVG conversion relies on GDI+ and is Windows‑only
                Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Operation skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }
}