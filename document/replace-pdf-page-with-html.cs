using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // original PDF
        const string htmlPath      = "page.html";    // HTML to convert
        const string outputPdfPath = "output.pdf";   // result PDF
        const int pageToReplace    = 2;              // 1‑based index of page to replace

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
            // Load the original PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Convert the HTML to a temporary PDF document
                Document htmlDoc;
                try
                {
                    // HtmlLoadOptions resides in Aspose.Pdf namespace
                    htmlDoc = new Document(htmlPath, new HtmlLoadOptions());
                }
                catch (TypeInitializationException)
                {
                    // HTML‑to‑PDF conversion requires GDI+ (Windows only)
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
                    return;
                }
                catch (DllNotFoundException)
                {
                    Console.WriteLine("GDI+ not found. HTML conversion unavailable on this platform.");
                    return;
                }

                // Ensure the HTML conversion produced at least one page
                if (htmlDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("HTML conversion produced no pages.");
                    return;
                }

                // Delete the target page if it exists (1‑based indexing)
                if (pageToReplace >= 1 && pageToReplace <= pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Delete(pageToReplace);
                }

                // Insert the first page from the HTML document at the desired position
                pdfDoc.Pages.Insert(pageToReplace, htmlDoc.Pages[1]);

                // Save the modified PDF (PDF is the default format)
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Page {pageToReplace} replaced and saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}