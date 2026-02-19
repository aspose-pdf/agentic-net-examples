using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source HTML file
        string htmlPath = "input.html";

        // Path for the resulting PDF file
        string pdfPath = "output.pdf";

        // Folder where extracted SVG files will be saved
        string svgFolder = "SvgFiles";

        // Verify that the HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Ensure the SVG output folder exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the HTML document
            Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions());

            // Convert and save as PDF (uses the document-save rule)
            htmlDoc.Save(pdfPath);

            // Extract SVG images from the generated PDF
            SvgExtractor extractor = new SvgExtractor();

            // Iterate through all pages and save each page's SVG content
            for (int i = 1; i <= htmlDoc.Pages.Count; i++)
            {
                Page page = htmlDoc.Pages[i];
                string svgPath = Path.Combine(svgFolder, $"page_{i}.svg");
                extractor.Extract(page, svgPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
            Console.WriteLine($"Extracted SVG files saved in: {svgFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}