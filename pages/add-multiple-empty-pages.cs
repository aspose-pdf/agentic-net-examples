using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List representing a request to add a page for each entry.
        List<int> pageRequests = new List<int> { 1, 2, 3, 4, 5 };

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            int pagesAdded = 0;
            foreach (int request in pageRequests)
            {
                // Evaluation mode allows a maximum of 4 pages in any collection.
                if (pagesAdded >= 4)
                {
                    Console.WriteLine("Evaluation limit reached – no more pages will be added.");
                    break;
                }

                // Add an empty page to the document.
                doc.Pages.Add();
                pagesAdded++;
                Console.WriteLine($"Added empty page #{pagesAdded} (request value: {request})");
            }

            // Save the resulting PDF.
            doc.Save("output.pdf");
            Console.WriteLine("PDF saved as output.pdf");
        }
    }
}