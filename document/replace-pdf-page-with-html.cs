using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For HTML load options (in same namespace)
using Aspose.Pdf;                    // HtmlLoadOptions is in Aspose.Pdf namespace

class ReplacePdfPageWithHtml
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // Existing PDF
        const string htmlPath      = "newpage.html"; // HTML to convert
        const string outputPdfPath = "result.pdf";   // PDF after replacement
        const int    pageNumber    = 2;              // 1‑based index of page to replace

        // Validate input files
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the original PDF
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Convert the HTML file to a temporary PDF document
                using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
                {
                    // Ensure the HTML produced at least one page
                    if (htmlDoc.Pages.Count == 0)
                    {
                        Console.Error.WriteLine("HTML conversion resulted in no pages.");
                        return;
                    }

                    // Extract the first (and only) page from the HTML‑derived PDF
                    Page newPage = htmlDoc.Pages[1];

                    // Remove the page to be replaced from the source PDF
                    // Page collections are 1‑based; Delete throws if index out of range
                    sourceDoc.Pages.Delete(pageNumber);

                    // Insert the new page at the same position
                    // Insert expects a 1‑based index where the new page will appear
                    sourceDoc.Pages.Insert(pageNumber, newPage);
                }

                // Save the modified PDF (uses the standard Save method – no extra SaveOptions needed)
                sourceDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Page {pageNumber} replaced successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}