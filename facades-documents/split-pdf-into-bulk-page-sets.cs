using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory where the split PDFs will be saved
        const string outputDir = "BulkSplits";

        // Define start‑end page pairs (1‑based indexing)
        // Evaluation mode of Aspose.PDF allows a maximum of 4 pages in any collection.
        // Therefore we create only 4 pages and split them into two bulks: 1‑2 and 3‑4.
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 2 },
            new int[] { 3, 4 }
        };

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Create a sample PDF in memory (4 pages) so the example is self‑contained
        // ---------------------------------------------------------------------
        using (MemoryStream inputPdfStream = new MemoryStream())
        {
            // Build a simple PDF with 4 blank pages (evaluation mode limit)
            using (Document doc = new Document())
            {
                for (int i = 0; i < 4; i++)
                {
                    doc.Pages.Add();
                }
                // Save the document to the memory stream
                doc.Save(inputPdfStream);
            }

            // Reset the stream position before passing it to the editor
            inputPdfStream.Position = 0;

            // Create the PdfFileEditor instance (it does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Split the input PDF into the defined bulks; each bulk is returned as a MemoryStream
            // Use the overload that works with streams to avoid file‑system dependencies
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputPdfStream, pageRanges);

            // Save each bulk stream to a separate file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outputPath = Path.Combine(outputDir, $"bulk_{i + 1}.pdf");

                // Ensure the stream is positioned at the beginning before writing
                bulkStreams[i].Position = 0;

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(fileStream);
                }

                // Dispose the MemoryStream now that it has been saved
                bulkStreams[i].Dispose();

                Console.WriteLine($"Saved bulk #{i + 1} to '{outputPath}'.");
            }
        }
    }
}
