using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "output.pdf";

        // List of page counts to add sequentially (example values)
        List<int> pageCounts = new List<int> { 2, 3, 1 };

        // Aspose.Pdf evaluation mode allows a maximum of 4 pages to be added.
        // The code below caps the total number of added pages to 4 to avoid the
        // runtime IndexOutOfRangeException that occurs when the limit is exceeded.
        const int maxPagesInEvalMode = 4;
        int pagesAdded = 0;

        // Create a new PDF document (empty). The default constructor creates a PDF
        // with a single blank page, which counts toward the evaluation limit.
        using (Document doc = new Document())
        {
            // The document already contains one page, so we start counting from 1.
            pagesAdded = doc.Pages.Count;

            // Iterate over each count and add that many empty pages, respecting the limit.
            foreach (int count in pageCounts)
            {
                // Determine how many pages we can still add without exceeding the limit.
                int remaining = maxPagesInEvalMode - pagesAdded;
                if (remaining <= 0)
                    break; // limit reached

                int toAdd = Math.Min(count, remaining);
                for (int i = 0; i < toAdd; i++)
                {
                    doc.Pages.Add();
                }
                pagesAdded += toAdd;
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with added pages saved to '{outputPath}'.");
    }
}
