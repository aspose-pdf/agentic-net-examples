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

        // Define bulk page sets (start and end page numbers, 1‑based indexing)
        int[][] bulks = new int[][]
        {
            new int[] { 1, 2 }, // pages 1‑2
            new int[] { 3, 5 }, // pages 3‑5
            new int[] { 6, 6 }  // page 6 alone
        };

        // Open the source PDF as a stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulks; each element is a MemoryStream containing a PDF document
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputStream, bulks);

            // Optional: save each bulk to a separate file for verification
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outPath = $"bulk_{i + 1}.pdf";

                // Ensure the stream position is at the beginning before copying
                bulkStreams[i].Position = 0;

                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Bulk {i + 1} saved to {outPath}");
            }
        }
    }
}