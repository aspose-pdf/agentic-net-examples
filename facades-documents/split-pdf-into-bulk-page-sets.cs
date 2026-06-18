using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfBulkSplitter
{
    /// <summary>
    /// Splits the specified PDF into multiple documents defined by start‑end page pairs.
    /// Each resulting document is saved to the output folder with a sequential file name.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="outputFolder">Folder where the split PDFs will be written.</param>
    /// <param name="pageRanges">
    /// Array of int[2] where each element contains the start page (inclusive) and end page (inclusive).
    /// Example: new int[][] { new int[] {1,3}, new int[] {4,6} }.
    /// </param>
    public static void SplitToBulks(string inputPdfPath, string outputFolder, int[][] pageRanges)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into the requested bulk page sets.
        // The method returns an array of MemoryStream, each containing a PDF document.
        MemoryStream[] bulkStreams = editor.SplitToBulks(inputPdfPath, pageRanges);

        // Save each MemoryStream to a separate file.
        for (int i = 0; i < bulkStreams.Length; i++)
        {
            string outputPath = Path.Combine(outputFolder, $"part{i + 1}.pdf");

            // Reset the position of the stream before reading.
            bulkStreams[i].Position = 0;

            // Write the stream content to a file.
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bulkStreams[i].CopyTo(fileStream);
            }

            // Dispose the individual MemoryStream after it has been saved.
            bulkStreams[i].Dispose();

            Console.WriteLine($"Saved bulk #{i + 1} to '{outputPath}'.");
        }
    }

    // Example usage.
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputDir = "SplitBulks";

        // Define start‑end page pairs (1‑based indexing as required by Aspose.Pdf).
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 },   // pages 1‑3
            new int[] { 4, 6 },   // pages 4‑6
            new int[] { 7, 10 }   // pages 7‑10
        };

        SplitToBulks(inputPdf, outputDir, ranges);
    }
}