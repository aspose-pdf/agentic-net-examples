using System;
using System.Collections.Generic;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // List defining how many empty pages to add in each iteration
        // (total pages are limited to 4 in evaluation mode)
        List<int> pageCounts = new List<int> { 2, 3, 1 };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            const int maxPagesInEvalMode = 4; // Aspose.PDF evaluation limit

            // Iterate over the list and add the specified number of empty pages each time
            foreach (int count in pageCounts)
            {
                for (int i = 0; i < count; i++)
                {
                    if (doc.Pages.Count >= maxPagesInEvalMode)
                        break; // stop adding when the limit is reached
                    doc.Pages.Add();
                }
                if (doc.Pages.Count >= maxPagesInEvalMode)
                    break; // exit outer loop as well
            }

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}