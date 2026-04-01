using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for License handling

class Program
{
    static void Main()
    {
        // Try to apply a license (if you have one). Without a license Aspose.PDF runs in evaluation mode
        // which limits any collection (Pages, Annotations, Bookmarks, etc.) to a maximum of 4 elements.
        bool hasLicense = false;
        try
        {
            License lic = new License();
            lic.SetLicense("Aspose.Pdf.lic"); // place your license file next to the executable or give a full path
            hasLicense = true;
        }
        catch
        {
            // No license – continue in evaluation mode (max 4 pages).
        }

        // Create a sample PDF
        using (Document doc = new Document())
        {
            int pagesToCreate = hasLicense ? 8 : 4; // evaluation mode cannot exceed 4 pages
            for (int i = 0; i < pagesToCreate; i++)
            {
                doc.Pages.Add();
            }

            // If we are running without a license we cannot safely move pages because the operation
            // would increase the page count beyond the 4‑element evaluation limit and throw an exception.
            // In that scenario we simply save the document as‑is.
            if (!hasLicense)
            {
                doc.Save("output.pdf");
                return;
            }

            // Determine the range we are allowed to move (licensed scenario)
            int firstPageToMove = 3; // 1‑based indexing as required by the task
            int lastPageToMove = 6;
            if (firstPageToMove > doc.Pages.Count) // safety guard for very small documents
                return;

            // Collect the pages that need to be moved
            List<Page> pagesToMove = new List<Page>();
            for (int i = firstPageToMove; i <= lastPageToMove; i++)
            {
                pagesToMove.Add(doc.Pages[i]);
            }

            // Append the collected pages at the end of the document.
            // Adding them one after another preserves the original order.
            foreach (Page p in pagesToMove)
            {
                // The Add method creates a copy of the page in the target document.
                doc.Pages.Add(p);
            }

            // Delete the original pages (the ones we just moved).
            // Deleting from the highest index downwards prevents index shifting problems.
            for (int i = lastPageToMove; i >= firstPageToMove; i--)
            {
                doc.Pages.Delete(i);
            }

            // Save the modified document
            doc.Save("output.pdf");
        }
    }
}
