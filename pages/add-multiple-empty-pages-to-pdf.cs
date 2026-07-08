using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List specifying how many empty pages to add in each step
        List<int> pageCounts = new List<int> { 2, 3, 1 };

        // Evaluation version of Aspose.Pdf allows only a limited number of pages (4).
        // Guard against exceeding this limit to avoid runtime IndexOutOfRangeException.
        const int maxPagesAllowed = 4;
        int pagesAdded = 0;

        // Create a new PDF document (empty)
        using (Document doc = new Document())
        {
            // Iterate over the list and add the required number of empty pages each time
            foreach (int count in pageCounts)
            {
                for (int i = 0; i < count && pagesAdded < maxPagesAllowed; i++)
                {
                    // Add() creates an empty page; size is chosen automatically
                    doc.Pages.Add();
                    pagesAdded++;
                }

                // Stop adding if we have reached the evaluation limit
                if (pagesAdded >= maxPagesAllowed)
                    break;
            }

            // Save the document; without SaveOptions this always writes a PDF
            doc.Save("multiple_pages.pdf");
        }

        Console.WriteLine("PDF with added pages saved to 'multiple_pages.pdf'.");
    }
}
