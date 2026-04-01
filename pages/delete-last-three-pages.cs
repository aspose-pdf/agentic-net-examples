using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with up to 4 pages (evaluation mode limit)
        using (Document doc = new Document())
        {
            // Add four pages (maximum allowed in evaluation mode)
            for (int i = 1; i <= 4; i++)
            {
                doc.Pages.Add();
            }

            Console.WriteLine("Initial page count: " + doc.Pages.Count);

            // Delete the last three pages (delete from highest index downwards)
            int pageCount = doc.Pages.Count;
            for (int i = pageCount; i > pageCount - 3 && i >= 1; i--)
            {
                doc.Pages.Delete(i);
                Console.WriteLine("Deleted page " + i);
            }

            Console.WriteLine("Final page count: " + doc.Pages.Count);

            // Save the resulting PDF
            doc.Save("output.pdf");
            Console.WriteLine("Saved output.pdf");
        }
    }
}