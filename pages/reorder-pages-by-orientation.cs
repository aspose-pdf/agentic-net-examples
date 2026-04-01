using System;
using System.Collections.Generic;
using Aspose.Pdf;

namespace ReorderPagesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with mixed orientations
            using (Document sampleDoc = new Document())
            {
                // Page 1 - Portrait
                Page page1 = sampleDoc.Pages.Add();
                page1.PageInfo.IsLandscape = false;

                // Page 2 - Landscape
                Page page2 = sampleDoc.Pages.Add();
                page2.PageInfo.IsLandscape = true;

                // Page 3 - Portrait
                Page page3 = sampleDoc.Pages.Add();
                page3.PageInfo.IsLandscape = false;

                // Page 4 - Landscape
                Page page4 = sampleDoc.Pages.Add();
                page4.PageInfo.IsLandscape = true;

                sampleDoc.Save("input.pdf");
            }

            // Reorder pages: landscape first, then portrait
            using (Document doc = new Document("input.pdf"))
            {
                // Build a new ordering of page numbers (1‑based as required by Aspose.Pdf)
                List<int> newOrder = new List<int>();

                // First collect landscape pages
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    if (doc.Pages[i].PageInfo.IsLandscape)
                        newOrder.Add(i);
                }

                // Then collect portrait pages
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    if (!doc.Pages[i].PageInfo.IsLandscape)
                        newOrder.Add(i);
                }

                // Aspose.Pdf does not expose a direct Reorder method on PageCollection.
                // Create a new document and copy pages in the desired order.
                Document reordered = new Document();
                foreach (int pageNumber in newOrder)
                {
                    // Add creates a copy of the source page, preserving its content and properties.
                    reordered.Pages.Add(doc.Pages[pageNumber]);
                }

                reordered.Save("output.pdf");
            }
        }
    }
}
