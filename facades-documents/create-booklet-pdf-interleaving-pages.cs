using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output PDF file path (booklet)
        const string outputPdf = "booklet_output.pdf";

        // Define left and right page sequences for the booklet (1‑based indices)
        int[] leftPages  = new int[] { 1, 2, 3, 4, 5 };
        int[] rightPages = new int[] { 6, 7, 8, 9, 10 };

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        Document srcDoc = new Document(inputPdf);

        // Validate page ranges
        int maxPage = srcDoc.Pages.Count;
        foreach (int p in leftPages)
            if (p < 1 || p > maxPage) { Console.Error.WriteLine($"Left page index out of range: {p}"); return; }
        foreach (int p in rightPages)
            if (p < 1 || p > maxPage) { Console.Error.WriteLine($"Right page index out of range: {p}"); return; }

        // Create a new document that will hold the booklet pages
        Document bookletDoc = new Document();
        // Ensure the document has a page size (use the size of the first source page)
        bookletDoc.PageInfo = srcDoc.PageInfo;

        // Interleave left and right pages as required for a booklet
        int pairCount = Math.Min(leftPages.Length, rightPages.Length);
        for (int i = 0; i < pairCount; i++)
        {
            // Add left page
            bookletDoc.Pages.Add(srcDoc.Pages[leftPages[i]]);
            // Add right page
            bookletDoc.Pages.Add(srcDoc.Pages[rightPages[i]]);
        }

        // If one side has extra pages, append them at the end
        if (leftPages.Length > pairCount)
        {
            for (int i = pairCount; i < leftPages.Length; i++)
                bookletDoc.Pages.Add(srcDoc.Pages[leftPages[i]]);
        }
        else if (rightPages.Length > pairCount)
        {
            for (int i = pairCount; i < rightPages.Length; i++)
                bookletDoc.Pages.Add(srcDoc.Pages[rightPages[i]]);
        }

        // Save the booklet PDF
        bookletDoc.Save(outputPdf);

        Console.WriteLine($"Booklet created successfully: {outputPdf}");
    }
}
