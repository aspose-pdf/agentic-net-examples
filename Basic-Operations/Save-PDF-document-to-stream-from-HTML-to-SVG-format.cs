using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Path to the HTML file to be converted.
        string htmlPath = Path.Combine(dataDir, "input.html");
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Stream that will receive the SVG output.
        using (MemoryStream svgStream = new MemoryStream())
        {
            // Load the HTML file into a PDF document.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Save the PDF document as SVG into the stream.
                SvgSaveOptions saveOptions = new SvgSaveOptions();
                pdfDoc.Save(svgStream, saveOptions);
            }

            // Reset the stream position for further reading or writing.
            svgStream.Position = 0;

            // Example: write the SVG stream to a file (optional, just to demonstrate usage).
            string svgPath = Path.Combine(dataDir, "output.svg");
            using (FileStream file = new FileStream(svgPath, FileMode.Create, FileAccess.Write))
            {
                svgStream.CopyTo(file);
            }

            Console.WriteLine($"SVG saved to stream and written to '{svgPath}'.");
        }
    }
}