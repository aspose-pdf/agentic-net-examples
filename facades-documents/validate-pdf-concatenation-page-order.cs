using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    // Extracts plain text from a specific page of a PDF document.
    static string ExtractPageText(Page page)
    {
        TextAbsorber absorber = new TextAbsorber();
        page.Accept(absorber);
        return absorber.Text ?? string.Empty;
    }

    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string mergedPdfPath = "merged.pdf";

        // Verify input files exist.
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the two source PDFs.
        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            int firstPageCount  = firstDoc.Pages.Count;   // 1‑based indexing
            int secondPageCount = secondDoc.Pages.Count;

            // Concatenate using PdfFileEditor (Facades API).
            PdfFileEditor editor = new PdfFileEditor();
            bool concatResult = editor.Concatenate(firstPdfPath, secondPdfPath, mergedPdfPath);
            if (!concatResult)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }

            // Load the merged PDF.
            using (Document mergedDoc = new Document(mergedPdfPath))
            {
                int mergedPageCount = mergedDoc.Pages.Count;
                int expectedCount   = firstPageCount + secondPageCount;

                // Validate total page count.
                if (mergedPageCount != expectedCount)
                {
                    Console.Error.WriteLine($"Page count mismatch. Expected {expectedCount}, got {mergedPageCount}.");
                    return;
                }

                // Validate order of pages from the first document.
                for (int i = 1; i <= firstPageCount; i++) // 1‑based
                {
                    string originalText = ExtractPageText(firstDoc.Pages[i]);
                    string mergedText   = ExtractPageText(mergedDoc.Pages[i]);

                    if (!originalText.Equals(mergedText, StringComparison.Ordinal))
                    {
                        Console.Error.WriteLine($"Page order mismatch at merged page {i} (should match first PDF page {i}).");
                        return;
                    }
                }

                // Validate order of pages from the second document.
                for (int i = 1; i <= secondPageCount; i++) // 1‑based
                {
                    int mergedPageIndex = firstPageCount + i;
                    string originalText = ExtractPageText(secondDoc.Pages[i]);
                    string mergedText   = ExtractPageText(mergedDoc.Pages[mergedPageIndex]);

                    if (!originalText.Equals(mergedText, StringComparison.Ordinal))
                    {
                        Console.Error.WriteLine($"Page order mismatch at merged page {mergedPageIndex} (should match second PDF page {i}).");
                        return;
                    }
                }

                Console.WriteLine("Concatenation preserved original page order successfully.");
            }
        }
    }
}