using System;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class SetPageSizeA4
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file to work with
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and ensure it has the required number of pages (limited to 4 due to evaluation mode)
            using (Document doc = new Document("input.pdf"))
            {
                while (doc.Pages.Count < 4)
                {
                    doc.Pages.Add();
                }

                // Set each page size to A4
                for (int pageIndex = 1; pageIndex <= 4; pageIndex++)
                {
                    doc.Pages[pageIndex].SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                }

                doc.Save("output.pdf");
            }
        }
    }
}
