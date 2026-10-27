using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Load the document to determine the total number of pages
            Document doc = new Document(inputPdf);
            int pageCount = doc.Pages.Count;

            // Build page‑range array where each range contains a single page (1‑based)
            int[][] pageRanges = new int[pageCount][];
            for (int i = 0; i < pageCount; i++)
            {
                pageRanges[i] = new int[] { i + 1 };
            }

            // Split the PDF into individual pages and obtain them as MemoryStream objects.
            // SplitToBulks returns an array of MemoryStream, one per defined range.
            MemoryStream[] pageStreams = editor.SplitToBulks(inputPdf, pageRanges);

            // Write each MemoryStream to a uniquely named PDF file
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Ensure the stream is positioned at the beginning
                MemoryStream ms = pageStreams[i];
                ms.Position = 0;

                string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");
                using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }

                // Dispose the stream after writing
                ms.Dispose();

                Console.WriteLine($"Saved page {i + 1} to '{outPath}'");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}
