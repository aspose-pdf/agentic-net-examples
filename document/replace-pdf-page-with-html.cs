using System;
using System.IO;
using Aspose.Pdf;

class ReplacePdfPageWithHtml
{
    static void Main()
    {
        // Input PDF, HTML source and output PDF paths.
        const string pdfPath   = "input.pdf";
        const string htmlPath  = "page.html";
        const string outputPdf = "output.pdf";

        // 1‑based index of the page to be replaced.
        const int pageIndexToReplace = 2;

        // Ensure source files exist.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML not found: {htmlPath}");
            return;
        }

        // Load the original PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the HTML content and convert it to a PDF document.
            // HtmlLoadOptions resides directly in the Aspose.Pdf namespace.
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // The HTML conversion creates a PDF with at least one page.
                // Grab the first (and only) generated page.
                Page generatedPage = htmlDoc.Pages[1];

                // Remove the page that should be replaced.
                // Aspose.Pdf uses 1‑based indexing for pages.
                pdfDoc.Pages.Delete(pageIndexToReplace);

                // Insert the generated page at the same position.
                pdfDoc.Pages.Insert(pageIndexToReplace, generatedPage);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Page {pageIndexToReplace} replaced successfully. Output saved to '{outputPdf}'.");
    }
}