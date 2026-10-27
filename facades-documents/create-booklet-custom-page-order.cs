using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source and booklet PDFs
        const string srcPath = "source.pdf";
        const string bookletPath = "booklet.pdf";

        // ------------------------------------------------------------
        // Step 1: Create a simple source PDF with identifiable page text
        // ------------------------------------------------------------
        using (Document srcDoc = new Document())
        {
            // Create 4 pages, each containing its page number as text
            for (int i = 1; i <= 4; i++)
            {
                Page page = srcDoc.Pages.Add();
                TextFragment tf = new TextFragment($"Page {i}");
                tf.TextState.FontSize = 24;
                page.Paragraphs.Add(tf);
            }

            srcDoc.Save(srcPath);
        }

        // ------------------------------------------------------------
        // Step 2: Define custom left and right page order arrays
        // ------------------------------------------------------------
        // Example: left side pages will be 2 and 4, right side pages 1 and 3
        int[] leftPages  = new int[] { 2, 4 };
        int[] rightPages = new int[] { 1, 3 };

        // ------------------------------------------------------------
        // Step 3: Generate the booklet manually (avoids PdfFileEditor.MakeBooklet runtime issue)
        // ------------------------------------------------------------
        // Load the source document
        Document srcDocument = new Document(srcPath);
        // Create an empty document that will become the booklet
        Document bookletDocument = new Document();
        // Ensure the booklet starts with a blank page collection
        bookletDocument.Pages.Delete();

        // Add pages according to the custom left/right arrays
        int sheetCount = Math.Max(leftPages.Length, rightPages.Length);
        for (int i = 0; i < sheetCount; i++)
        {
            // Left page (if defined)
            if (i < leftPages.Length)
            {
                int leftPageNumber = leftPages[i]; // 1‑based index
                // Add (copy) the page from the source to the booklet
                bookletDocument.Pages.Add(srcDocument.Pages[leftPageNumber]);
            }

            // Right page (if defined)
            if (i < rightPages.Length)
            {
                int rightPageNumber = rightPages[i]; // 1‑based index
                bookletDocument.Pages.Add(srcDocument.Pages[rightPageNumber]);
            }
        }

        // Save the generated booklet
        bookletDocument.Save(bookletPath);

        // ------------------------------------------------------------
        // Step 4: Load the resulting booklet and inspect page order
        // ------------------------------------------------------------
        using (Document bookletDoc = new Document(bookletPath))
        {
            Console.WriteLine("Booklet page order (extracted text per page):");

            // Iterate through pages (1‑based indexing)
            for (int i = 1; i <= bookletDoc.Pages.Count; i++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber();
                bookletDoc.Pages[i].Accept(absorber);
                string pageText = absorber.Text.Trim();

                // Output the page index and its extracted text
                Console.WriteLine($"Page {i}: \"{pageText}\"");
            }
        }

        // Cleanup: optional removal of temporary files
        // File.Delete(srcPath);
        // File.Delete(bookletPath);
    }
}
