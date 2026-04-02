using System;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Document doc = new Document())
            {
                // Aspose.PDF evaluation mode allows a maximum of 4 pages in any collection.
                // When you have a full license you can set this to 7 as the original task requires.
                int pageCount = 4; // Change to 7 after licensing.

                for (int i = 0; i < pageCount; i++)
                {
                    doc.Pages.Add();
                }

                // Get the target page (page 4 in evaluation mode, page 7 when licensed).
                Page targetPage = doc.Pages[pageCount];

                // Set the page size to Letter format using SetPageSize.
                targetPage.SetPageSize(PageSize.PageLetter.Width, PageSize.PageLetter.Height);

                // Save the modified PDF.
                doc.Save("output.pdf");
            }
        }
    }
}
