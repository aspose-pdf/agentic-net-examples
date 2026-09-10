using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where the split HTML pages and the index will be placed
        const string outputDirectory = "output_html";

        // Base name for the generated HTML files (Aspose.Pdf will append page numbers)
        const string baseHtmlFileName = "page.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Full path for the base HTML file (used only as a naming seed)
        string baseHtmlPath = Path.Combine(outputDirectory, baseHtmlFileName);

        // Load the PDF and convert it to HTML with one file per page
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true               // one HTML file per PDF page
                // Other options can be set here if needed
            };

            // Save – this creates multiple HTML files in the same folder as baseHtmlPath
            pdfDocument.Save(baseHtmlPath, htmlOptions);
        }

        // After conversion, collect all generated HTML page files (excluding the index if it already exists)
        var pageFiles = Directory.GetFiles(outputDirectory, "*_page*.html")
                                 .OrderBy(f => f)
                                 .ToList();

        // Fallback: if the naming pattern differs, take all .html files except index.html
        if (!pageFiles.Any())
        {
            pageFiles = Directory.GetFiles(outputDirectory, "*.html")
                                 .Where(f => !Path.GetFileName(f).Equals("index.html", StringComparison.OrdinalIgnoreCase))
                                 .OrderBy(f => f)
                                 .ToList();
        }

        // Create an index.html that links to each page file
        string indexPath = Path.Combine(outputDirectory, "index.html");
        using (StreamWriter writer = new StreamWriter(indexPath, false))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"UTF-8\">");
            writer.WriteLine("    <title>PDF Pages Index</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <h1>PDF Pages</h1>");
            writer.WriteLine("    <ul>");

            int pageNumber = 1;
            foreach (string filePath in pageFiles)
            {
                string fileName = Path.GetFileName(filePath);
                writer.WriteLine($"        <li><a href=\"{fileName}\">Page {pageNumber}</a></li>");
                pageNumber++;
            }

            writer.WriteLine("    </ul>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Conversion complete. HTML pages and index.html are located in '{outputDirectory}'.");
    }
}