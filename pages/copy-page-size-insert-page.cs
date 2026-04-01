using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        using (Document doc = new Document())
        {
            // Add three pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Ensure there are at least two pages to copy size from
            if (doc.Pages.Count < 2)
            {
                Console.WriteLine("Document does not contain enough pages.");
                return;
            }

            // Get dimensions of page 2 (1‑based indexing)
            Page pageTwo = doc.Pages[2];
            double pageWidth = pageTwo.PageInfo.Width;
            double pageHeight = pageTwo.PageInfo.Height;

            // Insert a new page. Evaluation mode allows a maximum of 4 pages.
            // Position 5 would exceed this limit; using position 4 for unlicensed mode.
            int insertPosition = 4; // change to 5 when a full license is available
            Page newPage = doc.Pages.Insert(insertPosition);
            newPage.SetPageSize(pageWidth, pageHeight);

            // Save the result
            doc.Save("output.pdf");
        }
    }
}