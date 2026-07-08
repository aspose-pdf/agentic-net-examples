using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for temporary files
        const string inputPdfPath  = "input_booklet.pdf";
        const string outputPdfPath = "output_booklet.pdf";

        // ------------------------------------------------------------
        // 1. Create a simple PDF with 4 pages (evaluation limit), each page contains its number.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            for (int i = 1; i <= 4; i++)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment($"Page {i}");
                tf.TextState.FontSize = 48;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.Position = new Position(100, 500);
                page.Paragraphs.Add(tf);
            }
            doc.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Define custom left and right page order for the booklet.
        //    Left pages will appear on the left side of each spread,
        //    right pages on the right side.
        // ------------------------------------------------------------
        // Evaluation version of Aspose.Pdf can process at most 4 pages in a single operation.
        // Therefore we limit the source document to 4 pages.
        int[] leftPages  = new int[] { 2, 4 };
        int[] rightPages = new int[] { 1, 3 };

        // ------------------------------------------------------------
        // 3. Create the booklet using the custom page arrays.
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPdfPath, outputPdfPath, leftPages, rightPages);

        if (!success)
        {
            Console.Error.WriteLine("MakeBooklet operation failed.");
            return;
        }

        // ------------------------------------------------------------
        // 4. Load the resulting booklet and extract the text of each page
        //    to verify the order matches the supplied arrays.
        // ------------------------------------------------------------
        using (Document resultDoc = new Document(outputPdfPath))
        {
            Console.WriteLine("Booklet page order (extracted text):");
            for (int pageIndex = 1; pageIndex <= resultDoc.Pages.Count; pageIndex++)
            {
                TextAbsorber absorber = new TextAbsorber();
                resultDoc.Pages[pageIndex].Accept(absorber);
                string pageText = absorber.Text.Trim();
                Console.WriteLine($"  Output Page {pageIndex}: \"{pageText}\"");
            }
        }

        // ------------------------------------------------------------
        // 5. Clean up temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(inputPdfPath);
            File.Delete(outputPdfPath);
        }
        catch
        {
            // Ignored – files may be in use or deletion may fail.
        }
    }
}
