using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file path (adjust as needed)
        const string htmlPath = "input.html";

        // Validate the HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document into an Aspose.Pdf Document
            // HtmlLoadOptions enables proper parsing of the HTML content
            Document pdfDocument = new Document(htmlPath, new HtmlLoadOptions());

            // Prepare a memory stream to hold the SVG output
            using (MemoryStream svgStream = new MemoryStream())
            {
                // Save the PDF document as SVG into the stream
                // SvgSaveOptions provides additional control over the SVG conversion
                pdfDocument.Save(svgStream, new SvgSaveOptions());

                // Reset stream position for any subsequent reading
                svgStream.Position = 0;

                // Example: write the SVG data to a file (optional)
                const string outputSvgPath = "output.svg";
                using (FileStream fileStream = new FileStream(outputSvgPath, FileMode.Create, FileAccess.Write))
                {
                    svgStream.CopyTo(fileStream);
                }

                Console.WriteLine($"SVG conversion successful. Output written to '{outputSvgPath}'.");
                Console.WriteLine($"Stream length: {svgStream.Length} bytes.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}