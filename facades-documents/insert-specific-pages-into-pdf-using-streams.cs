using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Output PDF path (result of the insertion)
        const string outputPdfPath = "result.pdf";

        // Pages from the source PDF that should be inserted (1‑based indices)
        // Evaluation mode allows a maximum of 4 pages in any collection, so we keep the total ≤ 4.
        int[] pagesToInsert = new int[] { 1, 2 };

        // Position in the destination PDF where the pages will be inserted (1‑based)
        int insertLocation = 2;

        // ---------------------------------------------------------------------
        // Create a sample destination PDF in memory (so we don't depend on a file)
        // ---------------------------------------------------------------------
        using (MemoryStream destPdfStream = new MemoryStream())
        {
            Document destDoc = new Document();
            // Add only 2 blank pages so that after insertion the total does not exceed 4.
            for (int i = 0; i < 2; i++)
                destDoc.Pages.Add();
            destDoc.Save(destPdfStream, SaveFormat.Pdf);
            destPdfStream.Position = 0; // rewind for reading

            // ---------------------------------------------------------------
            // Create a sample source PDF in memory (contains at least the pages we will insert)
            // ---------------------------------------------------------------
            using (MemoryStream srcPdfStream = new MemoryStream())
            {
                Document srcDoc = new Document();
                // Add 2 pages – the same number we will insert.
                for (int i = 0; i < 2; i++)
                    srcDoc.Pages.Add();
                srcDoc.Save(srcPdfStream, SaveFormat.Pdf);
                srcPdfStream.Position = 0; // rewind for reading

                // -----------------------------------------------------------
                // Perform the insertion using PdfFileEditor.Insert (stream overload)
                // -----------------------------------------------------------
                using (FileStream outStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    PdfFileEditor editor = new PdfFileEditor();
                    bool result = editor.Insert(destPdfStream, insertLocation, srcPdfStream, pagesToInsert, outStream);

                    if (!result)
                    {
                        Console.Error.WriteLine("Failed to insert pages.");
                    }
                    else
                    {
                        Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdfPath}'.");
                    }
                }
            }
        }
    }
}
