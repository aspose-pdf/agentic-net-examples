using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDFs
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Temporary aligned PDFs
        const string aligned1 = "first_aligned.pdf";
        const string aligned2 = "second_aligned.pdf";

        // Final merged output
        const string mergedOutput = "merged_aligned.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Desired uniform page size (A4) and margins (0.5 inch = 36 points)
        PageSize uniformSize = PageSize.A4;
        MarginInfo uniformMargin = new MarginInfo
        {
            Top = 36,
            Bottom = 36,
            Left = 36,
            Right = 36
        };

        // -----------------------------------------------------------------
        // Align pages of the first PDF using Document API (Aspose.Pdf)
        // -----------------------------------------------------------------
        Document doc1 = new Document(pdfPath1);
        foreach (Page page in doc1.Pages)
        {
            // Set uniform page dimensions
            page.PageInfo.Width = uniformSize.Width;
            page.PageInfo.Height = uniformSize.Height;
            // Apply uniform margins
            page.PageInfo.Margin = uniformMargin;
        }
        doc1.Save(aligned1);

        // -----------------------------------------------------------------
        // Align pages of the second PDF using Document API (Aspose.Pdf)
        // -----------------------------------------------------------------
        Document doc2 = new Document(pdfPath2);
        foreach (Page page in doc2.Pages)
        {
            page.PageInfo.Width = uniformSize.Width;
            page.PageInfo.Height = uniformSize.Height;
            page.PageInfo.Margin = uniformMargin;
        }
        doc2.Save(aligned2);

        // -----------------------------------------------------------------
        // Merge the two aligned PDFs using the documented Document pattern
        // -----------------------------------------------------------------
        using (Document target = new Document(aligned1))
        using (Document source = new Document(aligned2))
        {
            target.Pages.Add(source.Pages); // Append all pages from source
            target.Save(mergedOutput);      // Save the combined PDF
        }

        // Clean up temporary aligned files
        try { File.Delete(aligned1); } catch { /* ignore */ }
        try { File.Delete(aligned2); } catch { /* ignore */ }

        Console.WriteLine($"Merged PDF with aligned pages saved to '{mergedOutput}'.");
    }
}
