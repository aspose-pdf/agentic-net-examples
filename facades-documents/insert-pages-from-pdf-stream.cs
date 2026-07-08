using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        // Output PDF path (result of insertion)
        const string outputPdfPath = "output.pdf";

        // Create a destination PDF in memory (if you have a real file, replace this with a FileStream opened for read)
        using (MemoryStream destinationStream = new MemoryStream())
        {
            // Build a simple destination PDF with a single blank page
            Document destDoc = new Document();
            destDoc.Pages.Add();
            destDoc.Save(destinationStream, SaveFormat.Pdf);
            // Reset the stream position so the editor can read from the beginning
            destinationStream.Position = 0;

            // Create a source PDF in memory containing a few pages.
            // NOTE: When running without a licensed version of Aspose.Pdf the evaluation mode limits the
            // number of pages that can be processed (typically 2‑4 pages). To avoid the runtime
            // IndexOutOfRangeException shown in the original code we limit the source document to
            // two pages.
            using (MemoryStream sourceStream = new MemoryStream())
            {
                Document srcDoc = new Document();
                // Add only two pages – each page will contain a simple paragraph so we can see the difference
                for (int i = 1; i <= 2; i++)
                {
                    Page page = srcDoc.Pages.Add();
                    page.Paragraphs.Add(new TextFragment($"Source page {i}"));
                }
                srcDoc.Save(sourceStream, SaveFormat.Pdf);
                sourceStream.Position = 0;

                // Prepare the output stream (file on disk)
                using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    // PdfFileEditor does NOT implement IDisposable, so instantiate normally
                    PdfFileEditor editor = new PdfFileEditor();

                    // Insert pages 1 through 2 from sourceStream into destinationStream
                    // Insert location is 1‑based: 1 means insert before the first page of the destination PDF
                    bool success = editor.Insert(
                        destinationStream, // input PDF stream
                        1,                 // insert location (1‑based)
                        sourceStream,      // PDF stream providing pages to insert
                        1,                 // start page in source PDF (inclusive)
                        2,                 // end page in source PDF (inclusive)
                        outputStream);     // output PDF stream

                    Console.WriteLine(success ? "Pages inserted successfully." : "Page insertion failed.");
                }
            }
        }
    }
}
