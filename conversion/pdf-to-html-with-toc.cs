using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlBase = "output.html";   // base name for HTML files
        const string tocHtmlPath    = "toc.html";      // table of contents file
        const string outputFolder   = "HtmlOutput";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options.
                // SplitIntoPages = true creates one HTML file per PDF page.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages        = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode    = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion requires GDI+ on Windows. Wrap in try‑catch to handle non‑Windows platforms.
                try
                {
                    // Save the PDF as HTML files inside the output folder.
                    // The first page will be saved as "output.html",
                    // subsequent pages as "output_page2.html", "output_page3.html", etc.
                    string htmlOutputPath = Path.Combine(outputFolder, outputHtmlBase);
                    pdfDoc.Save(htmlOutputPath, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires GDI+ (Windows only). Skipping HTML generation.");
                    return;
                }

                // After conversion, generate a simple Table of Contents (TOC) linking to each page file.
                // The number of pages is known from the original PDF.
                using (StreamWriter tocWriter = new StreamWriter(Path.Combine(outputFolder, tocHtmlPath)))
                {
                    tocWriter.WriteLine("<!DOCTYPE html>");
                    tocWriter.WriteLine("<html lang=\"en\">");
                    tocWriter.WriteLine("<head><meta charset=\"UTF-8\"><title>Table of Contents</title></head>");
                    tocWriter.WriteLine("<body>");
                    tocWriter.WriteLine("<h1>Table of Contents</h1>");
                    tocWriter.WriteLine("<ul>");

                    // Page indexing in Aspose.Pdf is 1‑based.
                    for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                    {
                        // Determine the file name for the current page.
                        // Page 1 uses the base name; later pages get the "_pageN.html" suffix.
                        string pageFileName = pageNum == 1
                            ? outputHtmlBase
                            : $"output_page{pageNum}.html";

                        // Create a link to the page HTML file.
                        tocWriter.WriteLine($"  <li><a href=\"{pageFileName}\">Page {pageNum}</a></li>");
                    }

                    tocWriter.WriteLine("</ul>");
                    tocWriter.WriteLine("</body>");
                    tocWriter.WriteLine("</html>");
                }

                Console.WriteLine($"HTML conversion completed. Files are in '{outputFolder}'.");
                Console.WriteLine($"Table of Contents generated at '{Path.Combine(outputFolder, tocHtmlPath)}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}