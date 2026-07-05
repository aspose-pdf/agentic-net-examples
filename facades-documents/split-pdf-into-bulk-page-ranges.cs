using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Folder where the split PDFs will be saved
        const string outputFolder = "BulkOutputs";

        // Define start‑end page pairs (1‑based indexing)
        // Example: split into pages 1‑3, 4‑6, and 7‑10
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 4, 6 },
            new int[] { 7, 10 }
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into the defined bulk ranges
            MemoryStream[] bulkStreams = editor.SplitToBulks(inputPdf, pageRanges);

            // Save each resulting stream to a separate PDF file
            for (int i = 0; i < bulkStreams.Length; i++)
            {
                string outputPath = Path.Combine(outputFolder, $"bulk_{i + 1}.pdf");

                // Ensure the MemoryStream is positioned at the beginning
                bulkStreams[i].Position = 0;

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bulkStreams[i].CopyTo(fileStream);
                }

                // Dispose the MemoryStream after it has been written
                bulkStreams[i].Dispose();

                Console.WriteLine($"Saved bulk #{i + 1} to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}