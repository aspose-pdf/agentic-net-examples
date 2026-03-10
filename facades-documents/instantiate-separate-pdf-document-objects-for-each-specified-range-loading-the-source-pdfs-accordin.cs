using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePdf = "source.pdf";

        // Define the page ranges you want to extract.
        // Each inner array contains two integers: start page and end page (1‑based indexing).
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 5 },   // pages 4‑5
            new int[] { 6, 10 }   // pages 6‑10
        };

        // Ensure the source file exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Use PdfFileEditor to split the source PDF into separate streams for each range
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] splitStreams = editor.SplitToBulks(sourcePdf, ranges);

        // Process each resulting stream: load it into a Document and save it as an individual PDF
        for (int i = 0; i < splitStreams.Length; i++)
        {
            // Reset stream position before loading
            splitStreams[i].Position = 0;

            // Load the stream into a Document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(splitStreams[i]))
            {
                // Construct an output file name that reflects the range index
                string outputPath = $"output_part_{i + 1}.pdf";

                // Save the document (PDF format is the default when using a .pdf extension)
                doc.Save(outputPath);

                Console.WriteLine($"Saved range {i + 1} to '{outputPath}'.");
            }

            // Dispose the memory stream after use
            splitStreams[i].Dispose();
        }
    }
}