using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string bookletPath = "booklet.pdf";

        // -------------------------------------------------
        // 1. Create a simple PDF with 4 pages, each page
        //    contains its page number as visible text.
        // -------------------------------------------------
        using (Document srcDoc = new Document())
        {
            for (int i = 1; i <= 4; i++)
            {
                Page page = srcDoc.Pages.Add();
                TextFragment tf = new TextFragment($"Page {i}");
                tf.TextState.FontSize = 48;
                tf.Position = new Position(200, 700);
                page.Paragraphs.Add(tf);
            }
            srcDoc.Save(inputPath); // saves as PDF regardless of extension
        }

        // -------------------------------------------------
        // 2. Define custom left/right page order for booklet.
        //    Example: left pages = 2,4 ; right pages = 1,3
        // -------------------------------------------------
        int[] leftPages = new int[] { 2, 4 };
        int[] rightPages = new int[] { 1, 3 };

        // -------------------------------------------------
        // 3. Create booklet using PdfFileEditor (Facades API).
        //    PdfFileEditor does NOT implement IDisposable,
        //    so it is NOT wrapped in a using block.
        // -------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.MakeBooklet(inputPath, bookletPath, leftPages, rightPages);

        // -------------------------------------------------
        // 4. Open the resulting booklet and extract text from
        //    each page to verify the order.
        // -------------------------------------------------
        using (Document bookletDoc = new Document(bookletPath))
        {
            Console.WriteLine("Booklet page order (extracted text):");
            for (int i = 1; i <= bookletDoc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                bookletDoc.Pages[i].Accept(absorber);
                string pageText = absorber.Text.Trim();
                Console.WriteLine($"Page {i}: {pageText}");
            }
        }

        // Cleanup (optional)
        // File.Delete(inputPath);
        // File.Delete(bookletPath);
    }
}