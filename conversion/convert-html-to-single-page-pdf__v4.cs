using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file with default options.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a PDF document from the HTML source.
            Document doc = new Document(htmlPath, loadOptions);

            // ---------------------------------------------------------------------
            // Ensure the result is a single A4 page (595 x 842 points).
            // ---------------------------------------------------------------------
            // Set the size of the first page.
            Page firstPage = doc.Pages[1];
            firstPage.PageInfo.Width  = 595; // A4 width in points
            firstPage.PageInfo.Height = 842; // A4 height in points
            firstPage.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);

            // If the HTML produced more than one page, move its content to the first page
            // and delete the extra pages.
            for (int i = doc.Pages.Count; i > 1; i--)
            {
                Page extraPage = doc.Pages[i];
                foreach (var paragraph in extraPage.Paragraphs)
                {
                    firstPage.Paragraphs.Add(paragraph);
                }
                doc.Pages.Delete(i);
            }

            // ---------------------------------------------------------------------
            // Embed standard fonts to keep visual fidelity.
            // ---------------------------------------------------------------------
            // The FontEmbeddingMode property is not available in older Aspose.Pdf versions.
            // Setting EmbedStandardFonts to true ensures that the most common fonts are embedded.
            doc.EmbedStandardFonts = true;

            // Save the resulting PDF.
            doc.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.Error.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
