using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where the split PDFs will be saved
        const string outputDirectory = "BulkSplits";

        // Define start‑end page pairs (1‑based indexing)
        // Example: split into pages 1‑3, 4‑6, and 7‑10
        // NOTE: In Aspose.PDF evaluation mode a document can contain at most 4 pages.
        // Therefore we limit the ranges to the first 4 pages only.
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 4 } // trimmed to stay within the 4‑page limit
        };

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // Create a placeholder PDF if the expected input file does not exist.
        // The placeholder must contain at least as many pages as the highest
        // page number referenced in the (potentially trimmed) pageRanges array.
        // Evaluation mode caps the page count at 4, so we enforce that limit.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Determine the maximum page number required, but cap it at 4.
            int maxPage = pageRanges.Max(r => r.Length >= 2 ? r[1] : 0);
            int maxAllowed = Math.Min(maxPage, 4); // evaluation‑mode limit

            // Create a minimal PDF with the required number of blank pages.
            using (var placeholder = new Document())
            {
                for (int i = 0; i < maxAllowed; i++)
                {
                    placeholder.Pages.Add();
                }
                placeholder.Save(inputPdf);
            }
        }

        // Create the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the defined bulk page sets
        // Returns an array of MemoryStream, each containing a PDF document
        MemoryStream[] bulkStreams = editor.SplitToBulks(inputPdf, pageRanges);

        // Save each resulting MemoryStream to a separate file
        for (int i = 0; i < bulkStreams.Length; i++)
        {
            // Reset stream position before reading
            bulkStreams[i].Position = 0;

            // Build the output file name (e.g., bulk_1.pdf, bulk_2.pdf, ...)
            string outputPath = Path.Combine(outputDirectory, $"bulk_{i + 1}.pdf");

            // Write the stream content to the file
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bulkStreams[i].CopyTo(fileStream);
            }

            // Dispose the individual MemoryStream
            bulkStreams[i].Dispose();
        }

        Console.WriteLine("PDF split into bulk page sets completed.");
    }
}
