using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that will receive the inserted pages
        const string targetPdf = "target.pdf";

        // Output PDF after all insertions are done
        const string finalOutputPdf = "merged_output.pdf";

        // Ensure the target PDF exists – create an empty one if it does not.
        if (!File.Exists(targetPdf))
        {
            // Create a blank PDF with a single empty page (required by Insert operation).
            using (var blankDoc = new Document())
            {
                blankDoc.Pages.Add();
                blankDoc.Save(targetPdf);
            }
        }

        // Array of source PDF file paths
        string[] sourcePdfs = { "source1.pdf", "source2.pdf", "source3.pdf" };

        // Corresponding page numbers to insert from each source PDF
        // Each inner array contains the exact page numbers (1‑based) to take from the source file
        int[][] pagesToInsert = {
            new int[] { 2, 4 },          // from source1.pdf insert pages 2 and 4
            new int[] { 1, 3, 5 },       // from source2.pdf insert pages 1, 3 and 5
            new int[] { 7 }              // from source3.pdf insert page 7
        };

        // Validate that the arrays match
        if (sourcePdfs.Length != pagesToInsert.Length)
        {
            Console.Error.WriteLine("Source files and page ranges count mismatch.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // We'll work with a temporary file that is updated after each insertion
        string currentFile = targetPdf;

        // Insert position inside the current PDF (1‑based). 
        // Here we always insert at the beginning; adjust as needed.
        int insertPosition = 1;

        for (int i = 0; i < sourcePdfs.Length; i++)
        {
            // Verify source file exists before attempting insertion
            if (!File.Exists(sourcePdfs[i]))
            {
                Console.Error.WriteLine($"Source file '{sourcePdfs[i]}' not found.");
                return;
            }

            // Create a temporary file to hold the result of this insertion step
            string tempFile = Path.GetTempFileName();

            // Perform the insertion:
            //   inputFile      : current PDF (initially the target)
            //   insertLocation : where to insert pages (1 = beginning)
            //   portFile       : source PDF from which pages are taken
            //   pageNumber     : array of page numbers to insert
            //   outputFile     : temporary file that receives the result
            bool success = editor.Insert(
                currentFile,
                insertPosition,
                sourcePdfs[i],
                pagesToInsert[i],
                tempFile);

            if (!success)
            {
                Console.Error.WriteLine($"Insertion failed for source '{sourcePdfs[i]}'.");
                // Clean up temporary file before exiting
                File.Delete(tempFile);
                return;
            }

            // Delete the previous intermediate file (except the original target)
            if (currentFile != targetPdf && File.Exists(currentFile))
                File.Delete(currentFile);

            // The temporary file becomes the new current file for the next iteration
            currentFile = tempFile;
        }

        // Move the final temporary file to the desired output location
        // If a file already exists at the destination, overwrite it
        if (File.Exists(finalOutputPdf))
            File.Delete(finalOutputPdf);
        File.Move(currentFile, finalOutputPdf);

        Console.WriteLine($"Pages inserted successfully. Result saved to '{finalOutputPdf}'.");
    }
}
