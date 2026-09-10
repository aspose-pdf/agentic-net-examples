using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalInput = "input.pdf";          // source PDF (must exist)
        const string numberedInput = "numbered_input.pdf"; // temporary PDF with page numbers
        const string bookletOutput = "booklet_output.pdf";

        // -----------------------------------------------------------------
        // Step 1: Add visible page numbers to the original PDF.
        // This helps us verify the order after the booklet operation.
        // -----------------------------------------------------------------
        if (!File.Exists(originalInput))
        {
            Console.Error.WriteLine($"Source file not found: {originalInput}");
            return;
        }

        using (Document doc = new Document(originalInput))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Add a simple text fragment with the page number at the top‑left corner.
                TextFragment tf = new TextFragment($"Page {i}");
                tf.Position = new Position(10, doc.Pages[i].PageInfo.Height - 10); // 10 pts from left/top
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Black;
                doc.Pages[i].Paragraphs.Add(tf);
            }

            // Save the numbered version (used for the booklet operation).
            doc.Save(numberedInput);
        }

        // -----------------------------------------------------------------
        // Step 2: Define custom left and right page sequences for the booklet.
        // -----------------------------------------------------------------
        int[] leftPages  = new int[] { 2, 4, 6 };               // example left side pages
        int[] rightPages = new int[] { 1, 3, 5, 7 };           // example right side pages

        // -----------------------------------------------------------------
        // Step 3: Create the booklet using the custom page arrays.
        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(numberedInput, bookletOutput, leftPages, rightPages);
        if (!success)
        {
            Console.Error.WriteLine("MakeBooklet operation failed.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 4: Open the resulting booklet and extract the page‑number text
        // from each page to verify the ordering.
        // -----------------------------------------------------------------
        using (Document resultDoc = new Document(bookletOutput))
        {
            Console.WriteLine("Booklet page order (extracted page numbers):");
            for (int i = 1; i <= resultDoc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                resultDoc.Pages[i].Accept(absorber);
                string extracted = absorber.Text?.Trim() ?? "(no text)";
                Console.WriteLine($"Page {i}: {extracted}");
            }
        }

        // Cleanup temporary file (optional).
        try { File.Delete(numberedInput); } catch { /* ignore */ }

        Console.WriteLine("Verification completed.");
    }
}