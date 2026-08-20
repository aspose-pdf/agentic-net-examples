using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create PdfFileEditor instance (does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Use Document to determine the number of pages (required to build split ranges)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Build an array of page ranges, each range contains a single page (start, end)
            int[][] ranges = new int[pageCount][];
            for (int i = 0; i < pageCount; i++)
            {
                // Pages are 1‑based in Aspose.Pdf
                ranges[i] = new int[] { i + 1, i + 1 };
            }

            // Perform bulk split; each MemoryStream contains a separate PDF document
            MemoryStream[] splitStreams = editor.SplitToBulks(inputPath, ranges);

            // Iterate over the resulting streams and write each to a uniquely named file
            for (int i = 0; i < splitStreams.Length; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");

                // Reset stream position before reading
                splitStreams[i].Position = 0;

                // Write the stream content to a file
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    splitStreams[i].CopyTo(fs);
                }

                // Dispose the individual MemoryStream
                splitStreams[i].Dispose();

                Console.WriteLine($"Saved split PDF: {outPath}");
            }
        }

        // No explicit disposal needed for PdfFileEditor
    }
}