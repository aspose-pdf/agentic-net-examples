using System;
using System.IO;
using Aspose.Pdf; // Core PDF API – HtmlLoadOptions is in this namespace

class ReplacePdfPageWithHtml
{
    static void Main()
    {
        // Input files
        const string pdfInputPath  = "source.pdf";   // Existing PDF
        const string htmlInputPath = "newpage.html"; // HTML content to convert
        const string pdfOutputPath = "result.pdf";   // Output PDF with replaced page

        // Page number to replace (1‑based indexing as per Aspose.Pdf)
        const int pageToReplace = 2;

        // Validate input files
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the original PDF document
            using (Document sourceDoc = new Document(pdfInputPath))
            {
                // Load the HTML file and convert it to a PDF document
                // HtmlLoadOptions is required for HTML‑to‑PDF conversion
                using (Document htmlDoc = new Document(htmlInputPath, new HtmlLoadOptions()))
                {
                    // Ensure the source PDF actually has the page we want to replace
                    if (pageToReplace < 1 || pageToReplace > sourceDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageToReplace} is out of range. PDF has {sourceDoc.Pages.Count} pages.");
                        return;
                    }

                    // Get the first page generated from the HTML (could be multiple pages)
                    // For simplicity we take the first page; adjust as needed.
                    Page newPage = htmlDoc.Pages[1];

                    // Remove the target page from the source PDF
                    sourceDoc.Pages.Delete(pageToReplace);

                    // Insert the new page at the same position
                    // Insert uses 1‑based indexing; the new page will become pageToReplace
                    sourceDoc.Pages.Insert(pageToReplace, newPage);
                }

                // Save the modified PDF (uses the standard Save method – no extra SaveOptions needed)
                sourceDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Page {pageToReplace} replaced successfully. Output saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
