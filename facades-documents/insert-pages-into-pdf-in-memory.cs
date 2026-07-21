using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a destination PDF in memory (at least two pages for demo).
        // ---------------------------------------------------------------------
        byte[] destinationPdf;
        using (var destDoc = new Document())
        {
            // Add two blank pages.
            destDoc.Pages.Add();
            destDoc.Pages.Add();

            using (var ms = new MemoryStream())
            {
                destDoc.Save(ms);
                destinationPdf = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // 2. Create a source PDF in memory (e.g., three pages).
        // ---------------------------------------------------------------------
        byte[] sourcePdf;
        using (var srcDoc = new Document())
        {
            srcDoc.Pages.Add(); // page 1
            srcDoc.Pages.Add(); // page 2
            srcDoc.Pages.Add(); // page 3

            using (var ms = new MemoryStream())
            {
                srcDoc.Save(ms);
                sourcePdf = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // 3. Wrap the byte arrays in MemoryStreams and perform the insertion.
        // ---------------------------------------------------------------------
        using (MemoryStream destStream = new MemoryStream(destinationPdf))
        using (MemoryStream srcStream = new MemoryStream(sourcePdf))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Ensure streams are positioned at the beginning.
            destStream.Position = 0;
            srcStream.Position = 0;

            // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert after the first page of the destination (1‑based index).
            int insertLocation = 1;

            // Example: insert pages 2 and 3 from the source PDF.
            int[] pagesToInsert = new int[] { 2, 3 };

            bool result = editor.Insert(destStream, insertLocation, srcStream, pagesToInsert, outputStream);
            if (!result)
            {
                Console.Error.WriteLine("Failed to insert pages.");
                return;
            }

            // Reset the output stream so it can be read from the beginning.
            outputStream.Position = 0;

            // The merged PDF is now fully contained in outputStream.
            // For demonstration, write it to a file (optional).
            File.WriteAllBytes("merged.pdf", outputStream.ToArray());

            Console.WriteLine("Pages inserted successfully.");
        }
    }
}
