using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Input PDF file (must exist in the working directory)
    private const string inputPdf = "input.pdf";

    // Directory where HTML pages will be written
    private const string outputDir = "HtmlOutput";

    // Table of contents file – built at runtime, therefore static readonly
    private static readonly string tocPath = Path.Combine(outputDir, "toc.html");

    static void Main()
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Convert PDF to a set of HTML pages (one per PDF page)
                try
                {
                    pdfDoc.Save(outputDir, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion.");
                    return;
                }

                // Build a simple Table of Contents linking to each generated HTML page
                string[] pageFiles = Directory.GetFiles(outputDir, "page_*.html");
                Array.Sort(pageFiles); // ensure correct order

                using (StreamWriter writer = new StreamWriter(tocPath, false))
                {
                    writer.WriteLine("<!DOCTYPE html>");
                    writer.WriteLine("<html><head><meta charset=\"utf-8\"><title>Table of Contents</title></head><body>");
                    writer.WriteLine("<h1>Table of Contents</h1>");
                    writer.WriteLine("<ul>");

                    for (int i = 0; i < pageFiles.Length; i++)
                    {
                        string fileName = Path.GetFileName(pageFiles[i]);
                        writer.WriteLine($"  <li><a href=\"{fileName}\">Page {i + 1}</a></li>");
                    }

                    writer.WriteLine("</ul>");
                    writer.WriteLine("</body></html>");
                }

                Console.WriteLine($"HTML conversion completed. TOC created at: {tocPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
