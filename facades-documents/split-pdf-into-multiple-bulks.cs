using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Define bulk page ranges (start and end pages, 1‑based indexing)
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 },
            new int[] { 7, 10 }
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Split the PDF into the defined bulks
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputStream, ranges);

            // Example: save each bulk to a separate file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                // Ensure the stream is positioned at the beginning
                bulkStreams[i].Position = 0;

                string outputPath = $"bulk_{i + 1}.pdf";
                using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Bulk {i + 1} saved to '{outputPath}'");

                // Release the memory stream
                bulkStreams[i].Dispose();
            }
        }
    }
}