using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToHtmlWithToc
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Base name for generated HTML files (Aspose will create page‑wise files)
        const string htmlBasePath = "output.html";

        // Folder where the HTML files will be placed (same folder as the executable)
        string outputFolder = Path.GetDirectoryName(Path.GetFullPath(htmlBasePath)) ?? ".";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // -----------------------------------------------------------------
                // Configure HTML conversion options
                // -----------------------------------------------------------------
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Split each PDF page into a separate HTML file
                    SplitIntoPages = true,

                    // Embed all resources (images, CSS) into the HTML files
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Convert PDF to HTML (may throw TypeInitializationException on non‑Windows platforms)
                try
                {
                    pdfDoc.Save(htmlBasePath, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion.");
                    return;
                }

                // -----------------------------------------------------------------
                // Build a simple Table of Contents linking to each generated page
                // -----------------------------------------------------------------
                // Aspose creates files named: output.html (first page) and
                // output_page_2.html, output_page_3.html, ... for subsequent pages.
                // Gather all those files.
                string baseFileName = Path.GetFileNameWithoutExtension(htmlBasePath);
                string[] htmlFiles = Directory.GetFiles(outputFolder, $"{baseFileName}*.html")
                                              .OrderBy(f => f)
                                              .ToArray();

                // Create TOC HTML content
                string tocPath = Path.Combine(outputFolder, "index.html");
                using (StreamWriter writer = new StreamWriter(tocPath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("<!DOCTYPE html>");
                    writer.WriteLine("<html>");
                    writer.WriteLine("<head>");
                    writer.WriteLine("    <meta charset=\"UTF-8\">");
                    writer.WriteLine("    <title>Table of Contents</title>");
                    writer.WriteLine("</head>");
                    writer.WriteLine("<body>");
                    writer.WriteLine("    <h1>Table of Contents</h1>");
                    writer.WriteLine("    <ul>");

                    for (int i = 0; i < htmlFiles.Length; i++)
                    {
                        string fileName = Path.GetFileName(htmlFiles[i]);
                        writer.WriteLine($"        <li><a href=\"{fileName}\">Page {i + 1}</a></li>");
                    }

                    writer.WriteLine("    </ul>");
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }

                Console.WriteLine($"HTML conversion completed. TOC generated at: {tocPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
