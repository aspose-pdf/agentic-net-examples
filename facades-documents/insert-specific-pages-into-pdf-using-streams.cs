using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Pages to insert from the source PDF (1‑based page numbers)
        int[] pagesToInsert = new int[] { 1, 2 }; // keep total pages ≤ 4 for evaluation mode
        // Position in the destination PDF where pages will be inserted (1‑based)
        int insertPosition = 2; // after page 2 of the destination PDF
        const string outputPdfPath = "merged.pdf";

        // ------------------------------------------------------------
        // Build a destination PDF in memory (must contain at least insertPosition‑1 pages)
        // ------------------------------------------------------------
        using (MemoryStream destStream = new MemoryStream())
        {
            using (Document destDoc = new Document())
            {
                // Add 2 pages to the destination PDF (pages 1‑2)
                for (int i = 0; i < 2; i++)
                {
                    destDoc.Pages.Add();
                }
                destDoc.Save(destStream);
            }
            destStream.Position = 0; // reset for reading

            // ------------------------------------------------------------
            // Build a source PDF in memory (must contain the pages we want to insert)
            // ------------------------------------------------------------
            using (MemoryStream srcStream = new MemoryStream())
            {
                using (Document srcDoc = new Document())
                {
                    // Add 2 pages to the source PDF (pages 1‑2)
                    for (int i = 0; i < 2; i++)
                    {
                        srcDoc.Pages.Add();
                    }
                    srcDoc.Save(srcStream);
                }
                srcStream.Position = 0; // reset for reading

                // ------------------------------------------------------------
                // Perform the insertion using PdfFileEditor.Insert (stream overloads)
                // ------------------------------------------------------------
                using (MemoryStream outStream = new MemoryStream())
                {
                    PdfFileEditor editor = new PdfFileEditor();
                    bool result = editor.Insert(destStream, insertPosition, srcStream, pagesToInsert, outStream);

                    if (!result)
                    {
                        Console.Error.WriteLine("Failed to insert pages.");
                        return;
                    }

                    // Write the merged PDF to a physical file for verification
                    outStream.Position = 0;
                    using (FileStream fileOut = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                    {
                        outStream.CopyTo(fileOut);
                    }
                }
            }
        }
    }
}
