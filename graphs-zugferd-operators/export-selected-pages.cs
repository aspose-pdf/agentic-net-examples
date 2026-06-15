using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Suppress known vulnerability warning for demonstration purposes
        #pragma warning disable NU1903
        // Create a sample PDF with up to 4 pages (evaluation mode limit)
        using (Document sourceDoc = new Document())
        {
            for (int i = 1; i <= 4; i++)
            {
                Page page = sourceDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Page " + i);
                page.Paragraphs.Add(fragment);
            }
            sourceDoc.Save("source.pdf");
        }
        #pragma warning restore NU1903

        // Reopen the created PDF
        using (Document sourceDoc = new Document("source.pdf"))
        {
            // Pages that we want to export as separate PDFs (1‑based indexing)
            int[] pagesToExport = new int[] { 2, 4 };

            foreach (int pageNumber in pagesToExport)
            {
                // Create a new document for the selected page
                using (Document singlePageDoc = new Document())
                {
                    // Copy the page preserving its original size and orientation
                    Page originalPage = sourceDoc.Pages[pageNumber];
                    // Add creates a copy of the page in the target document
                    singlePageDoc.Pages.Add(originalPage);

                    // Save the new document
                    string outputFileName = "page_" + pageNumber + ".pdf";
                    singlePageDoc.Save(outputFileName);
                }
            }
        }
    }
}