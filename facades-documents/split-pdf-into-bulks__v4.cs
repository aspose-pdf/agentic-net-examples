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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Define bulk page ranges (start and end pages, 1‑based inclusive)
        int[][] bulkRanges = new int[2][];
        bulkRanges[0] = new int[] { 1, 3 };
        bulkRanges[1] = new int[] { 4, 5 };

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputStream, bulkRanges);

            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outputFileName = "bulk" + (i + 1).ToString() + ".pdf";
                using (FileStream outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].WriteTo(outputStream);
                }
                bulkStreams[i].Dispose();
                Console.WriteLine("Saved bulk document: " + outputFileName);
            }
        }
    }
}
