using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string outputPath = "concatenated.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Capture page dimensions from the first PDF
        List<(double Width, double Height)> firstPageDims = new List<(double, double)>();
        int firstPageCount;
        using (Document firstDoc = new Document(firstPath))
        {
            firstPageCount = firstDoc.Pages.Count;
            for (int i = 1; i <= firstPageCount; i++)
            {
                var info = firstDoc.Pages[i].PageInfo;
                firstPageDims.Add((info.Width, info.Height));
            }
        }

        // Capture page dimensions from the second PDF
        List<(double Width, double Height)> secondPageDims = new List<(double, double)>();
        int secondPageCount;
        using (Document secondDoc = new Document(secondPath))
        {
            secondPageCount = secondDoc.Pages.Count;
            for (int i = 1; i <= secondPageCount; i++)
            {
                var info = secondDoc.Pages[i].PageInfo;
                secondPageDims.Add((info.Width, info.Height));
            }
        }

        // Concatenate the two PDFs using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool concatResult = editor.Concatenate(firstPath, secondPath, outputPath);
        if (!concatResult)
        {
            Console.Error.WriteLine("Failed to concatenate PDFs.");
            return;
        }

        // Verify that the concatenated PDF preserves the original page order
        using (Document mergedDoc = new Document(outputPath))
        {
            int mergedPageCount = mergedDoc.Pages.Count;
            int expectedCount = firstPageCount + secondPageCount;

            if (mergedPageCount != expectedCount)
            {
                Console.WriteLine($"Page count mismatch: expected {expectedCount}, got {mergedPageCount}.");
                return;
            }

            bool orderPreserved = true;

            // Verify pages from the first document appear first
            for (int i = 1; i <= firstPageCount; i++)
            {
                var mergedInfo = mergedDoc.Pages[i].PageInfo;
                var expected = firstPageDims[i - 1];
                if (!DimensionsEqual(mergedInfo, expected))
                {
                    orderPreserved = false;
                    Console.WriteLine($"Page {i} does not match the corresponding page from the first PDF.");
                    break;
                }
            }

            // Verify pages from the second document follow
            if (orderPreserved)
            {
                for (int i = 1; i <= secondPageCount; i++)
                {
                    int mergedIndex = firstPageCount + i;
                    var mergedInfo = mergedDoc.Pages[mergedIndex].PageInfo;
                    var expected = secondPageDims[i - 1];
                    if (!DimensionsEqual(mergedInfo, expected))
                    {
                        orderPreserved = false;
                        Console.WriteLine($"Page {mergedIndex} does not match the corresponding page from the second PDF.");
                        break;
                    }
                }
            }

            Console.WriteLine(orderPreserved
                ? "Concatenation preserved the original page order."
                : "Page order was altered after concatenation.");
        }
    }

    // Helper method to compare page dimensions for equality
    private static bool DimensionsEqual(PageInfo info, (double Width, double Height) expected)
    {
        const double Tolerance = 0.001; // allow minor floating‑point differences
        return Math.Abs(info.Width - expected.Width) < Tolerance &&
               Math.Abs(info.Height - expected.Height) < Tolerance;
    }
}
