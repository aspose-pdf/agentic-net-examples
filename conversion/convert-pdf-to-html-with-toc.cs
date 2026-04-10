using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToHtmlWithToc
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder for HTML files
        const string outputFolder = "HtmlOutput";

        // Base name for generated HTML files (Aspose will append page numbers if SplitIntoPages is true)
        const string htmlBaseName = "document.html";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Guard all Aspose.Pdf operations that may require GDI+ (System.Drawing).
        // On non‑Windows platforms libgdiplus is usually missing, causing a
        // TypeInitializationException deep inside System.Drawing. We therefore
        // exit early when the OS is not Windows.
        // -----------------------------------------------------------------
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("HTML conversion (and PDF creation) requires GDI+ which is unavailable on this platform. Skipping execution.");
            return;
        }

        // If the source PDF does not exist, create a minimal sample PDF so the example can run out‑of‑the‑box.
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdf(inputPdfPath);
            Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
        }

        // Full path for the base HTML file (used as the first page file name)
        string htmlBasePath = Path.Combine(outputFolder, htmlBaseName);

        // Load the PDF document (using the recommended load pattern)
        Document pdfDoc;
        try
        {
            pdfDoc = new Document(inputPdfPath);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("GDI+ (libgdiplus) is not available – cannot load PDF for HTML conversion.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        using (pdfDoc)
        {
            // Configure HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate one HTML file per PDF page
                SplitIntoPages = true,

                // Embed all resources (images, CSS) into the HTML files
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            // Convert PDF to HTML (Windows‑only GDI+ operation, so wrap in try‑catch)
            try
            {
                pdfDoc.Save(htmlBasePath, htmlOptions);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (libgdiplus) is missing – HTML conversion cannot be performed on this platform.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during PDF‑to‑HTML conversion: {ex.Message}");
                return;
            }

            // -----------------------------------------------------------------
            // Generate a simple Table of Contents (TOC) linking to each page HTML
            // -----------------------------------------------------------------
            int pageCount = pdfDoc.Pages.Count;
            StringBuilder tocLines = new StringBuilder();

            tocLines.AppendLine("<!DOCTYPE html>");
            tocLines.AppendLine("<html lang=\"en\">");
            tocLines.AppendLine("<head>");
            tocLines.AppendLine("    <meta charset=\"UTF-8\">");
            tocLines.AppendLine("    <title>Table of Contents</title>");
            tocLines.AppendLine("</head>");
            tocLines.AppendLine("<body>");
            tocLines.AppendLine("    <h1>Table of Contents</h1>");
            tocLines.AppendLine("    <ul>");

            for (int i = 1; i <= pageCount; i++)
            {
                // Determine the file name for the current page
                string pageFileName = i == 1
                    ? htmlBaseName                                   // first page uses the base name
                    : $"{Path.GetFileNameWithoutExtension(htmlBaseName)}_{i - 1}{Path.GetExtension(htmlBaseName)}";

                // Create a link entry
                tocLines.AppendLine($"        <li><a href=\"{pageFileName}\">Page {i}</a></li>");
            }

            tocLines.AppendLine("    </ul>");
            tocLines.AppendLine("</body>");
            tocLines.AppendLine("</html>");

            // Write the TOC HTML file into the output folder
            string tocPath = Path.Combine(outputFolder, "toc.html");
            File.WriteAllText(tocPath, tocLines.ToString());

            Console.WriteLine($"Conversion completed. HTML files are in '{outputFolder}'.");
            Console.WriteLine($"Table of Contents generated at '{tocPath}'.");
        }
    }

    /// <summary>
    /// Creates a very small PDF containing a single page with a title.
    /// This helper is used only when the expected input file is missing so the sample can run without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple paragraph so the PDF is not empty.
            page.Paragraphs.Add(new TextFragment("Sample PDF generated by PdfToHtmlWithToc example."));
            doc.Save(path);
        }
    }

    // Helper that walks the InnerException chain to detect a missing native GDI+ library.
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
