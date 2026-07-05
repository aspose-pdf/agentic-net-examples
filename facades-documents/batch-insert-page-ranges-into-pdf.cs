using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the initial destination PDF.
        const string basePdfPath = "base.pdf";

        // Path where the final merged PDF will be saved.
        const string finalPdfPath = "merged_output.pdf";

        // Verify that the base PDF exists.
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }

        // Define the list of source PDFs together with the page range to insert
        // and the position (1‑based) at which the pages should be inserted.
        var insertions = new[]
        {
            new { SourcePath = "source1.pdf", StartPage = 2, EndPage = 4, InsertAt = 1 },
            new { SourcePath = "source2.pdf", StartPage = 1, EndPage = 3, InsertAt = 5 },
            // Add more entries as needed.
        };

        // If there are no insertions, simply copy the base PDF to the final location.
        if (insertions.Length == 0)
        {
            File.Copy(basePdfPath, finalPdfPath, true);
            Console.WriteLine($"No insertions required. Copied base PDF to '{finalPdfPath}'.");
            return;
        }

        // The file that will be used as input for the next iteration.
        string currentInputPath = basePdfPath;

        // Temporary file used to store the result of each insertion step.
        string tempOutputPath = Path.GetTempFileName();

        foreach (var item in insertions)
        {
            // Validate source PDF existence.
            if (!File.Exists(item.SourcePath))
            {
                Console.Error.WriteLine($"Source PDF not found: {item.SourcePath}");
                break;
            }

            // Aspose.Pdf uses 1‑based page indexing; ensure valid values.
            int startPage = Math.Max(1, item.StartPage);
            int endPage   = Math.Max(startPage, item.EndPage);
            int insertPos = Math.Max(1, item.InsertAt);

            // Perform the insertion using PdfFileEditor.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(
                currentInputPath,   // input (destination) PDF
                insertPos,          // position where pages will be inserted
                item.SourcePath,    // source PDF containing pages to insert
                startPage,          // first page in source PDF to insert
                endPage,            // last page in source PDF to insert
                tempOutputPath);    // output PDF for this step

            if (!success)
            {
                Console.Error.WriteLine($"Insertion failed for source '{item.SourcePath}'.");
                break;
            }

            // Clean up the previous intermediate file (if it was a temporary file).
            if (currentInputPath != basePdfPath && File.Exists(currentInputPath))
                File.Delete(currentInputPath);

            // Prepare for the next iteration.
            currentInputPath = tempOutputPath;
            tempOutputPath = Path.GetTempFileName();
        }

        // After processing all insertions, move the final result to the desired location.
        if (File.Exists(currentInputPath))
        {
            File.Copy(currentInputPath, finalPdfPath, true);
            // Delete the last temporary file if it is not the original base PDF.
            if (currentInputPath != basePdfPath && File.Exists(currentInputPath))
                File.Delete(currentInputPath);
        }

        // Delete the unused temporary file created after the last loop iteration.
        if (File.Exists(tempOutputPath))
            File.Delete(tempOutputPath);

        Console.WriteLine($"Batch insertion completed. Result saved to '{finalPdfPath}'.");
    }
}