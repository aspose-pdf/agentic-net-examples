using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with up to 4 pages (evaluation limit)
        using (Document doc = new Document())
        {
            // Add pages (max 4 due to evaluation mode)
            for (int i = 1; i <= 4; i++)
            {
                doc.Pages.Add();
            }

            // If the document has at least 7 pages, change page 7 size to Letter.
            if (doc.Pages.Count >= 7)
            {
                Page page = doc.Pages[7];
                PageSize letterSize = PageSize.PageLetter;
                page.SetPageSize(letterSize.Width, letterSize.Height);
                Console.WriteLine("Page 7 size set to Letter.");
            }
            else
            {
                Console.WriteLine("Document has less than 7 pages. In a full license scenario, ensure the PDF has at least 7 pages before resizing.");
            }

            // Save the document
            doc.Save("output.pdf");
        }
    }
}
