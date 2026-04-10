using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string mergedPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        // Concatenate the two PDFs using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool concatResult = editor.Concatenate(firstPdf, secondPdf, mergedPdf);
        if (!concatResult)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // Load source documents to obtain original page counts
        int firstPageCount, secondPageCount;
        using (Document doc1 = new Document(firstPdf))
        {
            firstPageCount = doc1.Pages.Count; // 1‑based indexing
        }
        using (Document doc2 = new Document(secondPdf))
        {
            secondPageCount = doc2.Pages.Count;
        }

        // Load the merged document and verify page order by checking total page count
        using (Document mergedDoc = new Document(mergedPdf))
        {
            int mergedPageCount = mergedDoc.Pages.Count;

            // Expected page count is the sum of the two source documents
            int expectedPageCount = firstPageCount + secondPageCount;

            if (mergedPageCount != expectedPageCount)
            {
                Console.Error.WriteLine($"Page count mismatch: expected {expectedPageCount}, got {mergedPageCount}");
                return;
            }

            // Optional deeper validation: compare first page of each source with corresponding pages in merged doc
            // Verify that page 1 of merged equals page 1 of first PDF
            bool orderValid = true;
            for (int i = 1; i <= firstPageCount; i++)
            {
                // Simple check: compare page dimensions (width/height) as a proxy for order
                Page srcPage = new Document(firstPdf).Pages[i];
                Page mergedPage = mergedDoc.Pages[i];
                if (srcPage.PageInfo.Width != mergedPage.PageInfo.Width ||
                    srcPage.PageInfo.Height != mergedPage.PageInfo.Height)
                {
                    orderValid = false;
                    break;
                }
            }
            // Verify that pages after first document correspond to second PDF
            if (orderValid)
            {
                for (int i = 1; i <= secondPageCount; i++)
                {
                    Page srcPage = new Document(secondPdf).Pages[i];
                    Page mergedPage = mergedDoc.Pages[firstPageCount + i];
                    if (srcPage.PageInfo.Width != mergedPage.PageInfo.Width ||
                        srcPage.PageInfo.Height != mergedPage.PageInfo.Height)
                    {
                        orderValid = false;
                        break;
                    }
                }
            }

            Console.WriteLine(orderValid
                ? "Concatenation preserved original page order."
                : "Page order validation failed.");
        }
    }
}