using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of page counts to add sequentially.
        // For example: first add 3 pages, then 2 pages, then 4 pages.
        List<int> pageCounts = new List<int> { 3, 2, 4 };

        // Create a new empty PDF document.
        // The Document implements IDisposable, so wrap it in a using block.
        using (Document doc = new Document())
        {
            // Aspose.Pdf evaluation mode allows a maximum of 4 pages.
            // Guard against exceeding this limit to avoid runtime IndexOutOfRangeException.
            const int maxPagesInEvalMode = 4;
            int existingPages = doc.Pages.Count; // a new Document starts with 1 page.
            int remainingAllowed = Math.Max(0, maxPagesInEvalMode - existingPages);
            int addedSoFar = 0;

            // Iterate over each desired page count.
            foreach (int count in pageCounts)
            {
                // Determine how many pages we can still add without breaking the limit.
                int canAdd = Math.Min(count, remainingAllowed - addedSoFar);
                for (int i = 0; i < canAdd; i++)
                {
                    doc.Pages.Add();
                }
                addedSoFar += canAdd;
                // If we have reached the allowed maximum, stop adding further pages.
                if (addedSoFar >= remainingAllowed)
                    break;
            }

            // Save the resulting PDF. No SaveOptions are needed for PDF output.
            string outputPath = "multiple_pages.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{Path.GetFullPath(outputPath)}'. Total pages: {doc.Pages.Count} (evaluation limit handled).");
        }
    }
}
