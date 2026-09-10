using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a destination PDF in memory (the PDF that will receive pages).
        // ------------------------------------------------------------
        byte[] destinationBytes;
        using (var destDoc = new Document())
        {
            // Add a single page with some sample content.
            var destPage = destDoc.Pages.Add();
            destPage.Paragraphs.Add(new TextFragment("Destination PDF – Page 1"));

            using (var ms = new MemoryStream())
            {
                destDoc.Save(ms);
                destinationBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Create a source PDF in memory (the PDF that provides pages to insert).
        // ------------------------------------------------------------
        byte[] sourceBytes;
        using (var srcDoc = new Document())
        {
            // Page 1 of source PDF.
            var srcPage1 = srcDoc.Pages.Add();
            srcPage1.Paragraphs.Add(new TextFragment("Source PDF – Page 1"));

            // Page 2 of source PDF – this is the page we will insert.
            var srcPage2 = srcDoc.Pages.Add();
            srcPage2.Paragraphs.Add(new TextFragment("Source PDF – Page 2 (to be inserted)"));

            using (var ms = new MemoryStream())
            {
                srcDoc.Save(ms);
                sourceBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 3. Perform the insertion using PdfFileEditor.Insert.
        // ------------------------------------------------------------
        using (MemoryStream destStream = new MemoryStream(destinationBytes))
        using (MemoryStream srcStream = new MemoryStream(sourceBytes))
        using (MemoryStream resultStream = new MemoryStream())
        {
            // Ensure streams are positioned at the beginning before the operation.
            destStream.Position = 0;
            srcStream.Position = 0;

            int insertLocation = 1;                 // Insert after the first page of the destination.
            int[] pagesToInsert = new int[] { 2 };   // Insert page 2 from the source PDF.

            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(destStream, insertLocation, srcStream, pagesToInsert, resultStream);

            if (!success)
            {
                Console.Error.WriteLine("Insert operation failed.");
                return;
            }

            // Reset the result stream so it can be read from the beginning.
            resultStream.Position = 0;

            // Optional: write the merged PDF to a file for verification.
            using (FileStream file = new FileStream("merged.pdf", FileMode.Create, FileAccess.Write))
            {
                resultStream.CopyTo(file);
            }

            Console.WriteLine("Pages inserted successfully.");
        }
    }
}
