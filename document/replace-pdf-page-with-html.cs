using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // not required but harmless
using Aspose.Pdf.Text;   // for HtmlLoadOptions if needed

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "source.pdf";   // existing PDF
        const string htmlPath      = "newpage.html"; // HTML to convert
        const string outputPdfPath = "result.pdf";   // PDF with replaced page
        const int    pageToReplace = 2;              // 1‑based page number to replace

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the original PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Ensure the requested page exists
                if (pageToReplace < 1 || pageToReplace > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageToReplace} is out of range (1‑{pdfDoc.Pages.Count}).");
                    return;
                }

                // Convert HTML to a temporary PDF document
                Document htmlPdf;
                try
                {
                    // HtmlLoadOptions triggers the HTML‑to‑PDF conversion.
                    // This operation requires GDI+ and therefore works only on Windows.
                    htmlPdf = new Document(htmlPath, new HtmlLoadOptions());
                }
                catch (TypeInitializationException)
                {
                    Console.Error.WriteLine("HTML‑to‑PDF conversion requires Windows (GDI+). Operation aborted.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to convert HTML to PDF: {ex.Message}");
                    return;
                }

                // The HTML conversion should produce at least one page.
                if (htmlPdf.Pages.Count == 0)
                {
                    Console.Error.WriteLine("HTML conversion resulted in an empty PDF.");
                    return;
                }

                // Remove the target page from the original PDF
                pdfDoc.Pages.Delete(pageToReplace);

                // Insert the new page at the same position
                // Pages.Insert expects a 1‑based index.
                pdfDoc.Pages.Insert(pageToReplace, htmlPdf.Pages[1]);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Page {pageToReplace} replaced successfully. Output saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}