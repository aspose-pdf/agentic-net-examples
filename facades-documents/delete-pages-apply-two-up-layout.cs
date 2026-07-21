using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfProcessor
{
    // Deletes specified pages from the input PDF stream,
    // then creates a 2‑up layout (2 columns, 1 row) and writes the result to the output stream.
    public static void DeletePagesAndApplyTwoUp(Stream inputPdfStream, Stream outputPdfStream, int[] pagesToDelete)
    {
        // Intermediate memory stream to hold the PDF after page deletion.
        using (MemoryStream intermediateStream = new MemoryStream())
        {
            // Step 1: Delete pages.
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputPdfStream, pagesToDelete, intermediateStream);

            // Reset position to the beginning before the next operation.
            intermediateStream.Position = 0;

            // Step 2: Apply 2‑up layout.
            // 2 columns (x), 1 row (y) creates a side‑by‑side (2‑up) page.
            editor.MakeNUp(intermediateStream, outputPdfStream, 2, 1);
        }
    }

    // Example usage.
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1️⃣ Create a sample input PDF in‑memory so the sandbox has a valid file.
        // ---------------------------------------------------------------------
        using (MemoryStream inputStream = new MemoryStream())
        {
            // Create a simple PDF with three pages (so we have something to delete).
            using (Document seedDoc = new Document())
            {
                // Add three blank pages.
                seedDoc.Pages.Add();
                seedDoc.Pages.Add();
                seedDoc.Pages.Add();
                // Save the seed PDF to the memory stream.
                seedDoc.Save(inputStream);
            }
            // Reset the stream position before processing.
            inputStream.Position = 0;

            // -----------------------------------------------------------------
            // 2️⃣ Define pages to delete (1‑based indexing as required by Aspose).
            // -----------------------------------------------------------------
            int[] pagesToRemove = new int[] { 2, 3 }; // delete the 2nd and 3rd pages

            // -----------------------------------------------------------------
            // 3️⃣ Process the PDF and write the 2‑up result to an output stream.
            // -----------------------------------------------------------------
            using (FileStream outputFile = new FileStream("output_2up.pdf", FileMode.Create, FileAccess.Write))
            {
                DeletePagesAndApplyTwoUp(inputStream, outputFile, pagesToRemove);
            }
        }

        Console.WriteLine("Processed PDF saved to 'output_2up.pdf'.");
    }
}
