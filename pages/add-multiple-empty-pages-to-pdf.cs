using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input: list where each value represents how many empty pages to add in that step
        List<int> pageCounts = new List<int> { 2, 3, 1 };

        const string outputPath = "output.pdf";

        // Evaluation (unlicensed) mode of Aspose.PDF can hold at most 4 elements in any collection
        // (Pages, Annotations, Bookmarks, etc.). To avoid the IndexOutOfRangeException we cap the
        // total number of pages added to 4. A full license removes this limitation.
        const int maxPagesInEvaluation = 4;
        int pagesAdded = 0;

        // Document creation and disposal must be wrapped in a using block (document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Iterate over the list and add the specified number of empty pages each time
            foreach (int count in pageCounts)
            {
                for (int i = 0; i < count && pagesAdded < maxPagesInEvaluation; i++)
                {
                    // PageCollection.Add() adds an empty page (adds the most common size if sizes vary)
                    doc.Pages.Add();
                    pagesAdded++;
                }

                // Stop adding pages once the evaluation limit is reached
                if (pagesAdded >= maxPagesInEvaluation)
                    break;
            }

            // Save the resulting PDF (Document.Save(string) writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with added pages saved to '{outputPath}'. Total pages added: {pagesAdded} (capped at {maxPagesInEvaluation} for evaluation mode).");
    }
}
