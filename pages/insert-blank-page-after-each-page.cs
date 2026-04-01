using System;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class InsertBlankPageAfterEachPage
    {
        static void Main(string[] args)
        {
            // NOTE: Aspose.PDF evaluation mode allows a maximum of 4 pages in a document.
            // To demonstrate the logic without a license we limit the sample to 2 pages.
            // With a full license you can remove the limits and work with any number of pages.

            // Create a sample PDF with a few pages (limited to 2 for evaluation mode)
            using (Document sampleDoc = new Document())
            {
                // Add two blank pages as sample content (2 * 2 = 4 pages after insertion)
                sampleDoc.Pages.Add();
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and insert a blank page after each existing page
            using (Document doc = new Document("input.pdf"))
            {
                // In evaluation mode we cannot exceed 4 pages, so we cap the original page count to 2.
                int maxOriginalPagesForEval = 2; // 2 original + 2 inserted = 4 pages total
                int originalPageCount = Math.Min(doc.Pages.Count, maxOriginalPagesForEval);

                // Iterate backwards to keep original page positions stable
                for (int i = originalPageCount; i >= 1; i--)
                {
                    // Insert an empty page after the current page (i + 1)
                    doc.Pages.Insert(i + 1);
                }

                doc.Save("output.pdf");
            }
        }
    }
}
