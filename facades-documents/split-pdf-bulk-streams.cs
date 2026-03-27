using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            using (var doc = new Document())
            {
                // Add a single blank page so the split logic has something to work with.
                doc.Pages.Add();
                doc.Save(inputPath);
                Console.WriteLine($"Created placeholder PDF '{inputPath}'.");
            }
        }

        // Determine the number of pages in the source PDF
        int pageCount;
        using (Document sourceDoc = new Document(inputPath))
        {
            pageCount = sourceDoc.Pages.Count;
        }

        // Build the bulk split definition – each sub‑array contains start and end page numbers (1‑based)
        int[][] bulks = new int[pageCount][];
        for (int i = 0; i < pageCount; i++)
        {
            bulks[i] = new int[2];
            bulks[i][0] = i + 1; // start page
            bulks[i][1] = i + 1; // end page (single‑page document)
        }

        // Perform the bulk split – each element of the array is a MemoryStream containing a PDF document
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToBulks(inputPath, bulks);

        // Write each stream to a uniquely named PDF file (page1.pdf, page2.pdf, ...)
        for (int i = 0; i < pageStreams.Length; i++)
        {
            string outputFile = $"page{i + 1}.pdf";
            MemoryStream ms = pageStreams[i];
            ms.Position = 0; // ensure we write from the beginning
            using (FileStream fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fileStream);
            }
            ms.Dispose();
            Console.WriteLine($"Saved {outputFile}");
        }
    }
}
