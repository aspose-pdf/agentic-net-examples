using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define bulk page sets (start page, end page). Pages are 1‑based.
        int[][] bulks = new int[][]
        {
            new int[] { 1, 3 }, // pages 1‑3
            new int[] { 4, 5 }, // pages 4‑5
            new int[] { 6, 6 }  // page 6 alone
        };

        MemoryStream[] bulkStreams;

        // Open the source PDF as a stream.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does not implement IDisposable, so no using needed.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks; each element is a MemoryStream.
            bulkStreams = editor.SplitToBulks(inputStream, bulks);
        }

        // Example: write each bulk to a separate file (optional).
        for (int i = 0; i < bulkStreams.Length; i++)
        {
            string outPath = $"bulk_{i + 1}.pdf";

            // Ensure the stream is positioned at the beginning before copying.
            bulkStreams[i].Position = 0;

            using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                bulkStreams[i].CopyTo(outFile);
            }

            Console.WriteLine($"Saved bulk {i + 1} to '{outPath}'.");
        }
    }
}