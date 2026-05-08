using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a source PDF with two pages. The second page will be
        //    moved to the target document. We set a custom size and a
        //    rotation on that page to demonstrate that the properties are
        //    preserved after the move.
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document())
        {
            // Page 1 – default size.
            sourceDoc.Pages.Add();

            // Page 2 – custom dimensions and rotation.
            Page pageToMove = sourceDoc.Pages.Add();
            pageToMove.PageInfo.Width = 500;   // points
            pageToMove.PageInfo.Height = 700;  // points
            // Aspose.Pdf.PageInfo does not expose a Rotate property.
            // Use the IsLandscape flag to indicate a 90° rotation.
            pageToMove.PageInfo.IsLandscape = true;

            // --------------------------------------------------------
            // 2. Create a target PDF that already contains a single page.
            // --------------------------------------------------------
            using (Document targetDoc = new Document())
            {
                targetDoc.Pages.Add(); // a placeholder page

                // ----------------------------------------------------
                // 3. Move the page.
                // ----------------------------------------------------
                const int pageIndexToMove = 2;   // 1‑based index in source
                const int insertPosition = 1;    // 1‑based index in target

                // Validate indexes.
                if (pageIndexToMove < 1 || pageIndexToMove > sourceDoc.Pages.Count)
                    throw new ArgumentOutOfRangeException(nameof(pageIndexToMove),
                        $"Source document has {sourceDoc.Pages.Count} pages.");

                if (insertPosition < 1 || insertPosition > targetDoc.Pages.Count + 1)
                    throw new ArgumentOutOfRangeException(nameof(insertPosition),
                        $"Target document can accept positions 1..{targetDoc.Pages.Count + 1}.");

                // Retrieve the page reference from the source document.
                Page page = sourceDoc.Pages[pageIndexToMove];

                // Insert the page into the target document. The Insert overload
                // copies the page together with its size, rotation and all
                // content, so the original appearance is retained.
                targetDoc.Pages.Insert(insertPosition, page);

                // Remove the original page from the source document.
                sourceDoc.Pages.Delete(pageIndexToMove);

                // ----------------------------------------------------
                // 4. Save the modified PDFs.
                // ----------------------------------------------------
                sourceDoc.Save("source_modified.pdf");
                targetDoc.Save("target_modified.pdf");
            }
        }

        Console.WriteLine("Page moved successfully.");
    }
}
