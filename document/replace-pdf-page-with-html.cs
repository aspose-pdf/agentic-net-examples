using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the HTML source, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string htmlPath = "newpage.html";
        const string outputPdfPath = "result.pdf";

        // 1‑based index of the page to replace
        const int pageToReplace = 2;

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

        // Load the existing PDF document
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Load the HTML file and let Aspose.Pdf convert it to a PDF document in memory
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Ensure the HTML conversion produced at least one page
                if (htmlDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("HTML conversion resulted in no pages.");
                    return;
                }

                // Remove the target page from the source PDF (if it exists)
                if (pageToReplace >= 1 && pageToReplace <= srcDoc.Pages.Count)
                {
                    srcDoc.Pages.Delete(pageToReplace);
                }

                // Insert the newly generated page at the same position
                // HtmlDoc.Pages[1] is the first (and only) page created from the HTML
                srcDoc.Pages.Insert(pageToReplace, htmlDoc.Pages[1]);

                // Save the modified PDF
                srcDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Page {pageToReplace} replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}