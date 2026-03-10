using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string htmlPath   = "input.html";
        const string pdfPath    = "output.pdf";
        const string svgFolder  = "ExtractedSvgs";

        // Validate input HTML file
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Ensure the folder for extracted SVG images exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the HTML document using HtmlLoadOptions (default constructor)
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the loaded document as PDF
                doc.Save(pdfPath);

                // Extract SVG images from each page of the generated PDF
                SvgExtractor extractor = new SvgExtractor();
                foreach (Page page in doc.Pages)
                {
                    // This will create SVG files inside the specified folder
                    extractor.Extract(page, svgFolder);
                }
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
            Console.WriteLine($"SVG images extracted to folder: {svgFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}