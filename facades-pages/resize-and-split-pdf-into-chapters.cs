using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the whole book
        const string inputPdf = "input.pdf";

        // Temporary file that will hold the uniformly resized pages
        const string resizedPdf = "resized.pdf";

        // Directory where each chapter PDF will be saved
        const string outputDir = "Chapters";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // 1. Resize all pages to a uniform size (e.g., 500x700 units)
        //    This adds equal margins around the original content.
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();

        // The overload ResizeContents(string inputFile, string outputFile, int[] pages, double newWidth, double newHeight)
        // Passing null for the pages array applies the operation to all pages.
        editor.ResizeContents(inputPdf, resizedPdf, null, 500, 700);

        // ------------------------------------------------------------
        // 2. Define chapter boundaries (start page, end page) as needed.
        //    Here we use a hard‑coded example; in a real scenario this could be
        //    read from a table of contents or another source.
        // ------------------------------------------------------------
        int[][] chapters = new int[][]
        {
            new int[] { 1, 5 },   // Chapter 1: pages 1‑5
            new int[] { 6, 12 },  // Chapter 2: pages 6‑12
            new int[] { 13, 20 }  // Chapter 3: pages 13‑20
        };

        // ------------------------------------------------------------
        // 3. Split the resized PDF into separate chapter PDFs.
        //    SplitToBulks returns an array of MemoryStream objects,
        //    each containing one chapter document.
        // ------------------------------------------------------------
        MemoryStream[] chapterStreams = editor.SplitToBulks(resizedPdf, chapters);

        // ------------------------------------------------------------
        // 4. Write each chapter stream to a distinct file.
        // ------------------------------------------------------------
        for (int i = 0; i < chapterStreams.Length; i++)
        {
            string chapterPath = Path.Combine(outputDir, $"Chapter_{i + 1}.pdf");
            using (FileStream fs = new FileStream(chapterPath, FileMode.Create, FileAccess.Write))
            {
                chapterStreams[i].WriteTo(fs);
            }
            Console.WriteLine($"Saved chapter {i + 1} to '{chapterPath}'.");
        }
    }
}