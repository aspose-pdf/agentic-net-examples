using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of page counts to add sequentially (example values)
        List<int> pageCounts = new List<int> { 2, 3, 1 };

        // Output PDF file path
        const string outputPath = "output.pdf";

        // Aspose.Pdf evaluation version allows a maximum of 4 pages to be added.
        // This constant protects the code from throwing an IndexOutOfRangeException
        // when the limit is exceeded.
        const int evaluationPageLimit = 4;
        int pagesAdded = 0;

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Iterate over each count and add that many empty pages, respecting the limit
            foreach (int count in pageCounts)
            {
                for (int i = 0; i < count && pagesAdded < evaluationPageLimit; i++)
                {
                    doc.Pages.Add();
                    pagesAdded++;
                }

                // Stop adding pages once the limit is reached
                if (pagesAdded >= evaluationPageLimit)
                    break;
            }

            // Save the document to the specified path
            doc.Save(outputPath);
        }

        // Report the result
        Console.WriteLine($"Created PDF with {pagesAdded} pages at '{outputPath}'.");
    }
}
