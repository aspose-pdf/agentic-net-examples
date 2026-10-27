using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdf1 = "first.pdf";
        const string pdf2 = "second.pdf";

        // Temporary files after aligning margins and page size
        const string aligned1 = "first_aligned.pdf";
        const string aligned2 = "second_aligned.pdf";

        // Final merged output
        const string merged = "merged.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(pdf1) || !File.Exists(pdf2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Desired uniform page size (A4) and margins (points)
        double pageWidth  = PageSize.A4.Width;
        double pageHeight = PageSize.A4.Height;
        double marginLeft   = 50; // 0.69 inch
        double marginBottom = 50;
        double marginRight  = 50;
        double marginTop    = 50;

        // Align each PDF to the same size and margins
        AlignPdf(pdf1, aligned1, pageWidth, pageHeight, marginLeft, marginBottom, marginRight, marginTop);
        AlignPdf(pdf2, aligned2, pageWidth, pageHeight, marginLeft, marginBottom, marginRight, marginTop);

        // Merge the two aligned PDFs using the Facades API
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(new[] { aligned1, aligned2 }, merged);

        Console.WriteLine($"Merged PDF saved to '{merged}'.");
    }

    // Adjusts page size and margins for all pages in a PDF
    static void AlignPdf(string inputPath, string outputPath,
                         double width, double height,
                         double left, double bottom, double right, double top)
    {
        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf convention)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Resize each page to the target dimensions via PageInfo
                Page page = doc.Pages[i];
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;

                // Apply uniform margins using MarginInfo
                page.PageInfo.Margin = new MarginInfo(left, right, top, bottom);
            }

            // Save the adjusted PDF; no SaveOptions needed for PDF output
            doc.Save(outputPath);
        }
    }
}
