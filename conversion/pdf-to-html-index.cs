using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtmlBase = "output.html"; // base name for split pages
        const string indexFile = "index.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Convert PDF to HTML, one HTML file per page
        using (Document pdfDoc = new Document(inputPdf))
        {
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true
            };
            pdfDoc.Save(outputHtmlBase, saveOptions);
        }

        // Locate generated HTML page files (exclude the index file itself)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputHtmlBase)) ?? Directory.GetCurrentDirectory();
        var pageFiles = Directory.GetFiles(outputDir, "*.html")
                                 .Where(f => !string.Equals(Path.GetFileName(f), Path.GetFileName(indexFile), StringComparison.OrdinalIgnoreCase))
                                 .OrderBy(f => f)
                                 .ToList();

        // Create index.html linking to each page file
        using (StreamWriter writer = new StreamWriter(Path.Combine(outputDir, indexFile)))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head><meta charset=\"UTF-8\"><title>PDF Pages Index</title></head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<h1>PDF Pages</h1>");
            writer.WriteLine("<ul>");
            foreach (string pagePath in pageFiles)
            {
                string fileName = Path.GetFileName(pagePath);
                writer.WriteLine($"<li><a href=\"{fileName}\">{fileName}</a></li>");
            }
            writer.WriteLine("</ul>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Conversion completed. Index file created: {indexFile}");
    }
}