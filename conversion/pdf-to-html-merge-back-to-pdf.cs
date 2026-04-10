using System;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf; // HtmlLoadOptions, Document, etc. are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string htmlOutputDir = "HtmlPages";
        const string htmlBaseFileName = "page.html"; // base name for split pages
        const string combinedHtmlFileName = "combined.html";
        const string finalPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the directory for HTML pages exists
        Directory.CreateDirectory(htmlOutputDir);
        string htmlBasePath = Path.Combine(htmlOutputDir, htmlBaseFileName);

        // -------------------------------------------------
        // 1. Convert PDF to HTML, one HTML file per page
        // -------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,                     // generate separate HTML per page
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // other options can be set here if needed
                };

                // This will create page.html, page_1.html, page_2.html, ... in the same folder
                pdfDoc.Save(htmlBasePath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML generation.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // 2. Combine the generated HTML pages into one file
        // -------------------------------------------------
        var htmlFiles = Directory.GetFiles(htmlOutputDir, "page*.html")
                                 .OrderBy(f => f) // alphabetical order matches page order
                                 .ToList();

        if (htmlFiles.Count == 0)
        {
            Console.Error.WriteLine("No HTML pages were generated.");
            return;
        }

        StringBuilder combinedHtml = new StringBuilder();
        combinedHtml.AppendLine("<!DOCTYPE html>");
        combinedHtml.AppendLine("<html>");
        combinedHtml.AppendLine("<head>");
        combinedHtml.AppendLine("<meta charset=\"UTF-8\">");
        combinedHtml.AppendLine("<title>Combined HTML</title>");
        combinedHtml.AppendLine("</head>");
        combinedHtml.AppendLine("<body>");

        foreach (string file in htmlFiles)
        {
            string content = File.ReadAllText(file);

            // Extract the inner <body> content to avoid nested <html>/<body> tags
            int bodyStart = content.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
            int bodyEnd = content.IndexOf("</body>", StringComparison.OrdinalIgnoreCase);

            if (bodyStart >= 0 && bodyEnd > bodyStart)
            {
                string bodyContent = content.Substring(bodyStart + 6, bodyEnd - (bodyStart + 6));
                combinedHtml.AppendLine(bodyContent);
            }
            else
            {
                // Fallback: append whole file if <body> tags are missing
                combinedHtml.AppendLine(content);
            }
        }

        combinedHtml.AppendLine("</body>");
        combinedHtml.AppendLine("</html>");

        string combinedHtmlPath = Path.Combine(htmlOutputDir, combinedHtmlFileName);
        File.WriteAllText(combinedHtmlPath, combinedHtml.ToString());

        // -------------------------------------------------
        // 3. Convert the combined HTML back to PDF
        // -------------------------------------------------
        try
        {
            using (Document htmlDoc = new Document(combinedHtmlPath, new HtmlLoadOptions()))
            {
                htmlDoc.Save(finalPdfPath);
            }

            Console.WriteLine($"Successfully created PDF: {finalPdfPath}");
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML‑to‑PDF conversion requires Windows (GDI+). Skipping final PDF generation.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML‑to‑PDF conversion: {ex.Message}");
        }
    }
}
