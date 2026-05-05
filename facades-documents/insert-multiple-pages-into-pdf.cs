using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Inserts pages from multiple source PDFs into a target PDF.
    // sourcePdfs[i]  – path to the i‑th source PDF.
    // pageRanges[i]  – array of 1‑based page numbers to take from sourcePdfs[i].
    // insertLocation – 1‑based position in the target where pages will be inserted.
    // outputPdf      – final result file.
    static void InsertMultiplePages(string targetPdf,
                                    string[] sourcePdfs,
                                    int[][] pageRanges,
                                    int insertLocation,
                                    string outputPdf)
    {
        if (!File.Exists(targetPdf))
            throw new FileNotFoundException($"Target PDF not found: {targetPdf}");

        if (sourcePdfs.Length != pageRanges.Length)
            throw new ArgumentException("sourcePdfs and pageRanges must have the same length.");

        // Working file that will be updated after each insertion.
        string workingFile = targetPdf;

        // Temporary file holder for the result of each insertion step.
        string tempFile = null;

        var editor = new PdfFileEditor();

        for (int i = 0; i < sourcePdfs.Length; i++)
        {
            string srcPdf = sourcePdfs[i];
            int[] pages = pageRanges[i];

            if (!File.Exists(srcPdf))
                throw new FileNotFoundException($"Source PDF not found: {srcPdf}");

            // Create a temporary file for the intermediate result.
            tempFile = Path.GetTempFileName();

            // Perform the insertion.
            // Insert inserts the specified pages from srcPdf into workingFile
            // after the page number 'insertLocation'.
            bool success = editor.Insert(workingFile,
                                         insertLocation,
                                         srcPdf,
                                         pages,
                                         tempFile);

            if (!success)
                throw new InvalidOperationException($"Insertion failed for source '{srcPdf}'.");

            // The newly created file becomes the working file for the next iteration.
            // Delete the previous intermediate file if it was a temporary one.
            if (workingFile != targetPdf && File.Exists(workingFile))
                File.Delete(workingFile);

            workingFile = tempFile;
        }

        // Move the final intermediate file to the desired output location.
        if (File.Exists(outputPdf))
            File.Delete(outputPdf);

        File.Move(workingFile, outputPdf);
    }

    static void Main()
    {
        // Example usage:
        // Target PDF into which pages will be inserted.
        const string targetPath = "target.pdf";

        // Source PDFs.
        string[] sources = { "source1.pdf", "source2.pdf", "source3.pdf" };

        // Corresponding page ranges (1‑based page numbers).
        int[][] ranges = {
            new int[] { 2, 4, 5 },   // pages from source1.pdf
            new int[] { 1, 3 },      // pages from source2.pdf
            new int[] { 6 }          // page from source3.pdf
        };

        // Insert after page 1 of the target PDF.
        int insertAfterPage = 1;

        // Output file.
        const string outputPath = "merged_output.pdf";

        try
        {
            InsertMultiplePages(targetPath, sources, ranges, insertAfterPage, outputPath);
            Console.WriteLine($"Pages inserted successfully. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}