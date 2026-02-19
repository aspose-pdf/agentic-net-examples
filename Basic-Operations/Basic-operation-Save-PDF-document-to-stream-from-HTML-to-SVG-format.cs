using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define paths (adjust as needed)
        string dataDir = "YOUR_DATA_DIRECTORY";
        string htmlPath = Path.Combine(dataDir, "input.html");

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML file into a PDF Document
            using (Document pdfDocument = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Prepare SVG save options (default options are sufficient for most cases)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the PDF as SVG into a memory stream
                using (MemoryStream svgStream = new MemoryStream())
                {
                    pdfDocument.Save(svgStream, svgOptions);

                    // Reset stream position for any subsequent reading
                    svgStream.Position = 0;

                    // Example: write the SVG stream to a file (optional)
                    string svgOutputPath = Path.Combine(dataDir, "output.svg");
                    using (FileStream file = new FileStream(svgOutputPath, FileMode.Create, FileAccess.Write))
                    {
                        svgStream.CopyTo(file);
                    }

                    Console.WriteLine($"SVG conversion completed successfully. Output saved to '{svgOutputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
