using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document (will contain a default empty page)
        using (Document doc = new Document())
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                doc.Pages.Add();
            }

            // Access the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Set custom size: width = 500 points, height = 700 points
            firstPage.SetPageSize(500.0, 700.0);

            // Save the modified document
            doc.Save("output.pdf");
            Console.WriteLine("First page size set to 500x700 points and saved to output.pdf");
        }
    }
}