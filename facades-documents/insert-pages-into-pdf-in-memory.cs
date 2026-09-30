using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create sample PDFs entirely in memory (no file I/O).
        // ------------------------------------------------------------
        Document sourceDoc = CreateSamplePdf("Source", 2);      // 2 pages source
        Document destDoc   = CreateSamplePdf("Destination", 3); // 3 pages destination

        // ------------------------------------------------------------
        // 2. Insert all pages from the source PDF after the first page
        //    of the destination PDF (1‑based index).
        // ------------------------------------------------------------
        int insertAt = 2; // position after page 1
        for (int i = 1; i <= sourceDoc.Pages.Count; i++)
        {
            destDoc.Pages.Insert(insertAt, sourceDoc.Pages[i]);
            insertAt++; // advance insertion point for the next page
        }

        // ------------------------------------------------------------
        // 3. Save the merged document to a MemoryStream.
        // ------------------------------------------------------------
        using (MemoryStream resultStream = new MemoryStream())
        {
            destDoc.Save(resultStream);
            resultStream.Position = 0; // reset for downstream consumption

            // (Optional) write the result to a file so you can verify it.
            File.WriteAllBytes("merged_output.pdf", resultStream.ToArray());
        }
    }

    /// <summary>
    /// Helper that creates a simple PDF with the specified title and page count.
    /// Each page contains a single TextFragment indicating its origin.
    /// </summary>
    static Document CreateSamplePdf(string title, int pageCount)
    {
        Document doc = new Document();
        for (int i = 1; i <= pageCount; i++)
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment($"{title} – Page {i}");
            tf.TextState.FontSize = 14;
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);
        }
        return doc;
    }
}
