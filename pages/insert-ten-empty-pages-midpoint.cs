using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // NOTE: In Aspose.PDF evaluation mode a collection (Pages, Annotations, etc.)
        // cannot contain more than 4 elements. The code below caps the number of pages
        // to stay within this limit. To insert the full ten pages you need a licensed
        // version of Aspose.PDF.

        // Create a sample PDF with 4 pages (max allowed in evaluation mode)
        using (Document sampleDoc = new Document())
        {
            int maxSamplePages = 4; // evaluation‑mode cap
            for (int i = 0; i < maxSamplePages; i++)
            {
                sampleDoc.Pages.Add();
            }
            sampleDoc.Save("input.pdf");
        }

        // Load the sample PDF and insert empty pages at its midpoint
        using (Document doc = new Document("input.pdf"))
        {
            int originalCount = doc.Pages.Count;
            int insertPosition = (originalCount / 2) + 1; // 1‑based index

            // Determine how many pages we can actually insert without breaking the 4‑page limit
            int maxTotalPages = 4; // evaluation‑mode limit
            int remainingSlots = maxTotalPages - originalCount;
            int pagesToInsert = Math.Max(0, Math.Min(10, remainingSlots));

            for (int i = 0; i < pagesToInsert; i++)
            {
                doc.Pages.Insert(insertPosition);
            }

            doc.Save("output.pdf");
        }
    }
}
