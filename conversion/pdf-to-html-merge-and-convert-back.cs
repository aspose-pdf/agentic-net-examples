using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string htmlFolderPath    = "HtmlPages";
        const string combinedHtmlPath  = "combined.html";
        const string outputPdfPath     = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for split HTML pages exists
        Directory.CreateDirectory(htmlFolderPath);

        // 1. Convert PDF to HTML pages (one HTML file per PDF page)
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages        = true, // generate separate HTML files
                    PartsEmbeddingMode    = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // The base file name; Aspose will create page.html, page_1.html, page_2.html, ...
                string baseHtmlPath = Path.Combine(htmlFolderPath, "page.html");
                pdfDoc.Save(baseHtmlPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only). Skip on unsupported platforms.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML generation.");
            return;
        }

        // 2. Combine the generated HTML pages into a single HTML file
        var htmlFiles = Directory.GetFiles(htmlFolderPath, "page*.html")
                                 .OrderBy(f => f) // ensures correct page order
                                 .ToArray();

        if (htmlFiles.Length == 0)
        {
            Console.Error.WriteLine("No HTML pages were generated.");
            return;
        }

        using (StreamWriter writer = new StreamWriter(combinedHtmlPath, false))
        {
            bool firstFile = true;
            foreach (string file in htmlFiles)
            {
                string content = File.ReadAllText(file);

                if (firstFile)
                {
                    // Write the full content of the first file (includes <html>, <head>, etc.)
                    writer.Write(content);
                    firstFile = false;
                }
                else
                {
                    // For subsequent files, strip the outer <html>/<head>/<body> tags to avoid nesting
                    // Simple approach: remove everything up to the first <body> tag and after </body>
                    int bodyStart = content.IndexOf("<body", StringComparison.OrdinalIgnoreCase);
                    if (bodyStart >= 0)
                    {
                        bodyStart = content.IndexOf('>', bodyStart) + 1;
                        int bodyEnd = content.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
                        if (bodyEnd > bodyStart)
                        {
                            string bodyContent = content.Substring(bodyStart, bodyEnd - bodyStart);
                            writer.WriteLine(bodyContent);
                        }
                    }
                }
            }
        }

        // 3. Convert the combined HTML back to PDF
        try
        {
            using (Document htmlDoc = new Document(combinedHtmlPath, new HtmlLoadOptions()))
            {
                // Save as PDF (default format)
                htmlDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully created PDF: {outputPdfPath}");
        }
        catch (TypeInitializationException)
        {
            // Loading HTML may also require GDI+ on non‑Windows platforms
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipping PDF generation.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML to PDF conversion: {ex.Message}");
        }
    }
}