using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "html_output";
        const string tocFileName = "toc.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load PDF and convert each page to a separate HTML file
        using (Document pdfDoc = new Document(inputPdf))
        {
            int pageCount = pdfDoc.Pages.Count;

            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true // one HTML file per PDF page
            };

            // Base name "page.html" will produce files page_1.html, page_2.html, ...
            pdfDoc.Save(Path.Combine(outputDir, "page.html"), htmlOptions);

            // Generate a simple Table of Contents linking to each page HTML file
            using (StreamWriter writer = new StreamWriter(Path.Combine(outputDir, tocFileName)))
            {
                writer.WriteLine("<!DOCTYPE html>");
                writer.WriteLine("<html><head><meta charset=\"UTF-8\"><title>Table of Contents</title></head><body>");
                writer.WriteLine("<h1>Table of Contents</h1>");
                writer.WriteLine("<ul>");
                for (int i = 1; i <= pageCount; i++)
                {
                    string pageFile = $"page_{i}.html";
                    writer.WriteLine($"<li><a href=\"{pageFile}\">Page {i}</a></li>");
                }
                writer.WriteLine("</ul>");
                writer.WriteLine("</body></html>");
            }
        }

        Console.WriteLine($"HTML pages and TOC generated in '{outputDir}'.");
    }
}