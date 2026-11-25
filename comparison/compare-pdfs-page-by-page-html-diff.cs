using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text; // needed for Rectangle

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffHtmlPath = "diff.html";

        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Define comparison options – we limit the extraction area to the whole A4 page.
            // In newer Aspose.Pdf versions ExtractionArea is a dedicated class, but in older
            // versions the property expects a Rectangle directly. Using a Rectangle works for
            // both because the compiler will pick the appropriate overload/property type.
            var options = new ComparisonOptions
            {
                ExtractionArea = new Rectangle(0, 0, 595, 842) // A4 size in points
            };

            // Perform page‑by‑page text comparison – returns a list of diff operations per page
            List<List<DiffOperation>> pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate an HTML diff report from the diff operations
            try
            {
                new HtmlDiffOutputGenerator().GenerateOutput(pageDiffs, diffHtmlPath);
                Console.WriteLine($"Diff HTML saved to '{diffHtmlPath}'.");
            }
            catch (Exception ex)
            {
                // HtmlDiffOutputGenerator may rely on GDI+ (Windows only). Handle gracefully on other platforms.
                Console.WriteLine($"Unable to generate HTML diff: {ex.Message}");
            }
        }
    }
}
