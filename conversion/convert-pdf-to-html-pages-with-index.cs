using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";                 // source PDF
        const string outDir    = "HtmlPages";                 // folder for HTML pages
        const string baseName  = "output";                    // base name for HTML files

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion – split each page into a separate file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML pages
                string firstPagePath = Path.Combine(outDir, $"{baseName}.html");
                pdfDoc.Save(firstPagePath, htmlOpts);
            }

            // After conversion, create an index.html that links to each page file
            string indexPath = Path.Combine(outDir, "index.html");
            using (StreamWriter writer = new StreamWriter(indexPath, false))
            {
                writer.WriteLine("<!DOCTYPE html>");
                writer.WriteLine("<html lang=\"en\">");
                writer.WriteLine("<head><meta charset=\"UTF-8\"><title>PDF Pages Index</title></head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<h1>PDF Pages</h1>");
                writer.WriteLine("<ul>");

                // Determine number of pages from the original PDF (same as number of HTML files)
                using (Document pdfDoc = new Document(pdfPath))
                {
                    int pageCount = pdfDoc.Pages.Count; // 1‑based indexing
                    for (int i = 1; i <= pageCount; i++)
                    {
                        string fileName = i == 1 ? $"{baseName}.html" : $"{baseName}_{i}.html";
                        writer.WriteLine($"  <li><a href=\"{fileName}\">Page {i}</a></li>");
                    }
                }

                writer.WriteLine("</ul>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }

            Console.WriteLine($"HTML pages and index created in '{outDir}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion uses GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}