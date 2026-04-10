using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where resized chapters will be saved
        const string outputDir = "Chapters";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Temporary file that will hold the uniformly resized PDF
        string resizedPdf = Path.Combine(outputDir, "resized_temp.pdf");

        // -----------------------------------------------------------------
        // Step 1: Resize the contents of the whole document.
        // ResizeContents(string inputFile, string outputFile, int[] pages, double newWidth, double newHeight)
        // Passing null for pages applies the operation to all pages.
        // The newWidth and newHeight are specified in default space units (points).
        // Here we shrink the page contents to 90% of their original size.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.ResizeContents(inputPdf, resizedPdf, null, 0.9, 0.9);

        // -----------------------------------------------------------------
        // Step 2: Determine chapter boundaries.
        // For demonstration we split the document into chapters of 10 pages each.
        // The actual page count is obtained from a Document instance.
        // -----------------------------------------------------------------
        int totalPages;
        using (Document doc = new Document(resizedPdf))
        {
            totalPages = doc.Pages.Count; // 1‑based page count
        }

        const int pagesPerChapter = 10;
        int chapterCount = (totalPages + pagesPerChapter - 1) / pagesPerChapter;

        // Build the int[][] required by SplitToBulks.
        // Each inner array contains two elements: start page and end page (inclusive).
        int[][] chapterRanges = new int[chapterCount][];
        for (int i = 0; i < chapterCount; i++)
        {
            int start = i * pagesPerChapter + 1;                     // 1‑based start page
            int end   = Math.Min(start + pagesPerChapter - 1, totalPages);
            chapterRanges[i] = new int[] { start, end };
        }

        // -----------------------------------------------------------------
        // Step 3: Split the resized PDF into separate chapter files.
        // SplitToBulks returns an array of MemoryStream, each containing a PDF.
        // -----------------------------------------------------------------
        MemoryStream[] chapterStreams = editor.SplitToBulks(resizedPdf, chapterRanges);

        // -----------------------------------------------------------------
        // Step 4: Write each chapter stream to a distinct file.
        // -----------------------------------------------------------------
        for (int i = 0; i < chapterStreams.Length; i++)
        {
            // Ensure the stream position is at the beginning before copying
            chapterStreams[i].Position = 0;

            string chapterPath = Path.Combine(outputDir, $"Chapter_{i + 1}.pdf");
            using (FileStream fs = new FileStream(chapterPath, FileMode.Create, FileAccess.Write))
            {
                chapterStreams[i].CopyTo(fs);
            }

            // Dispose the individual MemoryStream after saving
            chapterStreams[i].Dispose();

            Console.WriteLine($"Saved chapter {i + 1} to '{chapterPath}'.");
        }

        // Optional: clean up the temporary resized PDF
        if (File.Exists(resizedPdf))
        {
            File.Delete(resizedPdf);
        }

        Console.WriteLine("PDF splitting and resizing completed successfully.");
    }
}