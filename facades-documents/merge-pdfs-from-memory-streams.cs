using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Destination file path for the merged PDF
        const string outputPdfPath = "merged.pdf";

        // ---------------------------------------------------------------------
        // 1. Create two sample PDFs entirely in memory (no files on disk).
        // ---------------------------------------------------------------------
        MemoryStream firstStream;
        MemoryStream secondStream;

        // First PDF – contains a single page with some text.
        using (var firstDoc = new Document())
        {
            firstDoc.Pages.Add();
            firstDoc.Pages[1].Paragraphs.Add(new TextFragment("This is the FIRST PDF"));
            firstStream = new MemoryStream();
            firstDoc.Save(firstStream);
            // Reset the position so the stream can be read from the beginning.
            firstStream.Position = 0;
        }

        // Second PDF – also a single page with different text.
        using (var secondDoc = new Document())
        {
            secondDoc.Pages.Add();
            secondDoc.Pages[1].Paragraphs.Add(new TextFragment("This is the SECOND PDF"));
            secondStream = new MemoryStream();
            secondDoc.Save(secondStream);
            secondStream.Position = 0;
        }

        // ---------------------------------------------------------------------
        // 2. Concatenate the two in‑memory PDFs and write the result directly to a file.
        // ---------------------------------------------------------------------
        using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            var editor = new PdfFileEditor
            {
                // Close the input streams automatically after concatenation.
                CloseConcatenatedStreams = true
            };

            // The overload that works with streams writes the merged PDF straight to outputStream.
            editor.Concatenate(firstStream, secondStream, outputStream);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}
