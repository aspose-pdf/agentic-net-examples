using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path for the resulting PDF
        const string outputPath = "output.pdf";

        // List defining how many empty pages to add in each iteration
        // Example: first add 2 pages, then 3 pages, then 1 page
        List<int> pagesToAdd = new List<int> { 2, 3, 1 };

        // Aspose evaluation version allows a maximum of 4 pages to be added.
        // If you have a licensed version you can remove this limit.
        const int maxPagesAllowed = 4;

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Iterate over the list and add the specified number of empty pages
            foreach (int count in pagesToAdd)
            {
                for (int i = 0; i < count; i++)
                {
                    // Stop adding pages once the evaluation limit is reached
                    if (doc.Pages.Count >= maxPagesAllowed)
                    {
                        Console.WriteLine($"Evaluation limit of {maxPagesAllowed} pages reached. Remaining pages are skipped.");
                        break;
                    }
                    // Add() creates an empty page using the most common page size in the document
                    doc.Pages.Add();
                }
                // If limit already reached, exit outer loop as well
                if (doc.Pages.Count >= maxPagesAllowed)
                    break;
            }

            // Save the document – explicit SaveOptions are not required for PDF output
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with added pages saved to '{outputPath}'.");
    }
}
