using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types (Document, HtmlLoadOptions, etc.) are in this namespace

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";          // existing PDF
        const string htmlFile  = "newpage.html";        // HTML to convert
        const int    replacePage = 2;                    // 1‑based page index to replace
        const string outputPdf = "result.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(htmlFile))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlFile}");
            return;
        }

        ReplacePdfPageWithHtml(sourcePdf, htmlFile, replacePage, outputPdf);
        Console.WriteLine($"Page {replacePage} replaced and saved to '{outputPdf}'.");
    }

    /// <summary>
    /// Replaces a page in an existing PDF with a page generated from HTML content.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF.</param>
    /// <param name="htmlPath">Path to the HTML file.</param>
    /// <param name="pageNumber">1‑based index of the page to replace.</param>
    /// <param name="outputPath">Path where the resulting PDF will be saved.</param>
    static void ReplacePdfPageWithHtml(string pdfPath, string htmlPath, int pageNumber, string outputPath)
    {
        // Load the original PDF.
        using (Document sourceDoc = new Document(pdfPath))
        {
            // Convert the HTML to a PDF document. HtmlLoadOptions resides in Aspose.Pdf namespace.
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Validate page number.
                if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
                    throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

                // Ensure the HTML conversion produced at least one page.
                if (htmlDoc.Pages.Count == 0)
                    throw new InvalidOperationException("HTML conversion resulted in no pages.");

                // Remove the target page from the source document.
                sourceDoc.Pages.Delete(pageNumber);

                // Insert the first page generated from HTML at the same position.
                // Insert inserts BEFORE the specified index, so we insert at the original index.
                sourceDoc.Pages.Insert(pageNumber, htmlDoc.Pages[1]);

                // Save the modified document.
                sourceDoc.Save(outputPath);
            }
        }
    }
}
