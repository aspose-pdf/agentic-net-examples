using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample source PDF with two pages
        using (Document sourceDoc = new Document())
        {
            // First page (default orientation)
            sourceDoc.Pages.Add();

            // Second page with a rotation of 90 degrees
            Page rotatedPage = sourceDoc.Pages.Add();
            rotatedPage.Rotate = Rotation.on90; // Fixed rotation enum value

            sourceDoc.Save("source.pdf");
        }

        // Create an empty target PDF (it must contain at least one page)
        using (Document targetDoc = new Document())
        {
            targetDoc.Pages.Add();

            // Load the source PDF that contains the page to be moved
            using (Document sourceDoc = new Document("source.pdf"))
            {
                int pageNumberToMove = 2; // 1‑based index of the page to move

                // Retrieve the page object from the source document
                Page page = sourceDoc.Pages[pageNumberToMove];

                // Insert the page into the target document at the end
                int insertPosition = targetDoc.Pages.Count + 1; // 1‑based position
                targetDoc.Pages.Insert(insertPosition, page);

                // Optionally delete the page from the source document to complete the "move"
                sourceDoc.Pages.Delete(pageNumberToMove);
                sourceDoc.Save("source_modified.pdf");
            }

            // Save the target document that now contains the moved page
            targetDoc.Save("target.pdf");
        }
    }
}