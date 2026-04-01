using System;
using Aspose.Pdf;

namespace InsertPagesExample
{
    class Program
    {
        static void Main()
        {
            string outputPath = "output.pdf";

            // Create a new PDF document. In evaluation mode Aspose.Pdf allows a maximum of 4 pages total.
            using (Document document = new Document())
            {
                // Determine how many pages we can actually have in the document.
                const int evaluationPageLimit = 4;

                // Start with an empty document (0 pages).
                int currentPageCount = document.Pages.Count; // will be 0

                // Number of pages we would like to insert at the midpoint.
                int pagesToInsert = 10;

                // Calculate the maximum number of pages we can add without exceeding the evaluation limit.
                int maxInsertable = evaluationPageLimit - currentPageCount;
                int actualInsertCount = Math.Min(pagesToInsert, maxInsertable);

                // Insert pages one by one. After each insertion the document size grows, so we recalculate the middle position.
                for (int i = 0; i < actualInsertCount; i++)
                {
                    // Middle position is 1‑based. For an empty document the middle is 1.
                    int middlePosition = (document.Pages.Count / 2) + 1;
                    document.Pages.Insert(middlePosition);
                }

                // Save the resulting PDF.
                document.Save(outputPath);
                Console.WriteLine($"Inserted {actualInsertCount} empty pages at the calculated middle positions.");
                Console.WriteLine($"Total pages after insertion: {document.Pages.Count}");
            }
        }
    }
}
