using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base PDF that will receive the inserted pages
        const string basePdf = "base.pdf";
        // Desired final output file name
        const string finalPdf = "merged.pdf";

        if (!File.Exists(basePdf))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdf}");
            return;
        }

        // Source PDFs and the page ranges (inclusive) to insert from each one
        string[] sourcePdfs = new string[] { "source1.pdf", "source2.pdf", "source3.pdf" };
        int[][] pageRanges = new int[][]
        {
            new int[] { 1, 2 },   // pages 1‑2 from source1.pdf
            new int[] { 3, 5 },   // pages 3‑5 from source2.pdf
            new int[] { 2, 4 }    // pages 2‑4 from source3.pdf
        };

        if (sourcePdfs.Length != pageRanges.Length)
        {
            Console.Error.WriteLine("Source files and page ranges count mismatch.");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so no using block is required
        PdfFileEditor fileEditor = new PdfFileEditor();

        // The file that will be used as input for the next iteration
        string currentInput = basePdf;
        string intermediateOutput = "";

        for (int i = 0; i < sourcePdfs.Length; i++)
        {
            string sourceFile = sourcePdfs[i];
            int startPage = pageRanges[i][0];
            int endPage = pageRanges[i][1];

            if (!File.Exists(sourceFile))
            {
                Console.Error.WriteLine($"Source PDF not found: {sourceFile}");
                continue;
            }

            // Determine the insert location – append after the last page of the current document
            int insertLocation;
            using (Document doc = new Document(currentInput))
            {
                insertLocation = doc.Pages.Count + 1; // 1‑based indexing
            }

            // Create a unique temporary output name for this step
            intermediateOutput = $"merged_step{i}.pdf";

            bool success = fileEditor.Insert(currentInput, insertLocation, sourceFile, startPage, endPage, intermediateOutput);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from {sourceFile}.");
                return;
            }

            // Delete the previous intermediate file (except the original base PDF)
            if (currentInput != basePdf && File.Exists(currentInput))
            {
                File.Delete(currentInput);
            }

            // The newly created file becomes the input for the next iteration
            currentInput = intermediateOutput;
        }

        // Rename the final intermediate file to the desired output name
        if (File.Exists(intermediateOutput))
        {
            if (File.Exists(finalPdf))
            {
                File.Delete(finalPdf);
            }
            File.Move(intermediateOutput, finalPdf);
            Console.WriteLine($"Merged PDF saved as '{finalPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Merging process did not produce an output file.");
        }
    }
}