using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "concatenated.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load source documents to obtain page counts
        int firstPageCount;
        int secondPageCount;
        using (Document doc1 = new Document(firstPdf))
        {
            firstPageCount = doc1.Pages.Count;
        }
        using (Document doc2 = new Document(secondPdf))
        {
            secondPageCount = doc2.Pages.Count;
        }

        // Concatenate the PDFs using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(new string[] { firstPdf, secondPdf }, outputPdf);

        // Verify the concatenated document
        using (Document result = new Document(outputPdf))
        {
            int totalPages = result.Pages.Count;
            int expectedTotal = firstPageCount + secondPageCount;
            Console.WriteLine($"Expected total pages: {expectedTotal}, actual: {totalPages}");
            if (totalPages != expectedTotal)
            {
                Console.Error.WriteLine("Page count mismatch after concatenation.");
                return;
            }

            // Compare page dimensions to ensure order is preserved
            using (Document src1 = new Document(firstPdf))
            using (Document src2 = new Document(secondPdf))
            {
                bool orderPreserved = true;
                // First source pages
                for (int i = 1; i <= firstPageCount; i++)
                {
                    Page srcPage = src1.Pages[i];
                    Page outPage = result.Pages[i];
                    if (!PageDimensionsEqual(srcPage, outPage))
                    {
                        orderPreserved = false;
                        Console.Error.WriteLine($"Page {i} differs from the first source PDF.");
                        break;
                    }
                }
                // Second source pages
                if (orderPreserved)
                {
                    for (int i = 1; i <= secondPageCount; i++)
                    {
                        int outIndex = firstPageCount + i;
                        Page srcPage = src2.Pages[i];
                        Page outPage = result.Pages[outIndex];
                        if (!PageDimensionsEqual(srcPage, outPage))
                        {
                            orderPreserved = false;
                            Console.Error.WriteLine($"Page {outIndex} differs from the second source PDF.");
                            break;
                        }
                    }
                }

                Console.WriteLine(orderPreserved
                    ? "Page order preserved after concatenation."
                    : "Page order NOT preserved after concatenation.");
            }
        }
    }

    static bool PageDimensionsEqual(Page p1, Page p2)
    {
        // Compare width and height from PageInfo
        return p1.PageInfo.Width == p2.PageInfo.Width &&
               p1.PageInfo.Height == p2.PageInfo.Height;
    }
}