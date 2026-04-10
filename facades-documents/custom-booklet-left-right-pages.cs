using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // original PDF
        const string numberedPath = "numbered.pdf";         // PDF with page numbers added
        const string bookletPath = "booklet.pdf";           // result of MakeBooklet

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Add a visible page number to each page (for later verification)
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)               // 1‑based indexing
            {
                // Create a text fragment that shows the original page number
                TextFragment tf = new TextFragment($"Page {i}");
                tf.Position = new Position(20, doc.Pages[i].PageInfo.Height - 20); // top‑left corner
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                doc.Pages[i].Paragraphs.Add(tf);
            }

            // Save the temporary PDF that now contains page numbers
            doc.Save(numberedPath);
        }

        // ------------------------------------------------------------
        // Step 2: Create a customized booklet using left/right page arrays
        // ------------------------------------------------------------
        int[] leftPages  = new int[] { 2, 4, 6 };               // example left side pages
        int[] rightPages = new int[] { 1, 3, 5, 7 };            // example right side pages

        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(numberedPath, bookletPath, leftPages, rightPages);

        if (!success)
        {
            Console.Error.WriteLine("MakeBooklet operation failed.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Load the resulting booklet and extract the page‑number text
        //         to verify the order of pages after booklet creation.
        // ------------------------------------------------------------
        using (Document booklet = new Document(bookletPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            booklet.Pages.Accept(absorber);                     // extract text from all pages

            // The absorber contains concatenated text from all pages.
            // Split it per page to display the order.
            string[] pageTexts = absorber.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("Page order in the generated booklet (as extracted from page numbers):");
            for (int i = 0; i < pageTexts.Length; i++)
            {
                // Each line should be like "Page X"
                Console.WriteLine($"Booklet page {i + 1}: {pageTexts[i].Trim()}");
            }
        }
    }
}