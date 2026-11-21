using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "HtmlPages";
        const string baseHtmlName = "page.html"; // base name for generated pages

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF and convert each page to a separate HTML file
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true, // one HTML file per page
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML pages. The first page will be "page.html",
                // subsequent pages will be "page_1.html", "page_2.html", etc.
                string htmlOutputPath = Path.Combine(outputFolder, baseHtmlName);
                pdfDoc.Save(htmlOutputPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion is not supported on this platform.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // Generate an index.html that links to each page file
        try
        {
            // Get all generated HTML files (exclude the index file itself)
            var pageFiles = Directory.GetFiles(outputFolder, "*.html")
                                     .Where(f => !Path.GetFileName(f).Equals("index.html", StringComparison.OrdinalIgnoreCase))
                                     .OrderBy(f => f) // alphabetical order matches page order
                                     .ToList();

            string indexPath = Path.Combine(outputFolder, "index.html");
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

                foreach (string pageFile in pageFiles)
                {
                    string fileName = Path.GetFileName(pageFile);
                    writer.WriteLine($"        <li><a href=\"{fileName}\">{fileName}</a></li>");
                }

                writer.WriteLine("    </ul>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }

            Console.WriteLine($"Index generated at: {indexPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating index.html: {ex.Message}");
        }
    }
}