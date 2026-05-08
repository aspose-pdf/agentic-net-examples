using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchInsertPages
{
    static void Main()
    {
        // Paths
        const string basePdfPath = "base.pdf";          // Destination PDF that will receive inserts
        const string finalOutputPath = "merged_output.pdf";

        // Define source PDFs and the page ranges to insert (inclusive)
        // Example: insert pages 2‑4 from source1.pdf, then pages 1‑3 from source2.pdf, etc.
        var sources = new (string Path, int StartPage, int EndPage)[]
        {
            ("source1.pdf", 2, 4),
            ("source2.pdf", 1, 3),
            ("source3.pdf", 5, 7)
        };

        // Verify base PDF exists
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }

        // Verify each source PDF exists
        foreach (var src in sources)
        {
            if (!File.Exists(src.Path))
            {
                Console.Error.WriteLine($"Source PDF not found: {src.Path}");
                return;
            }
        }

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Working file that will be updated after each insertion
        string currentFile = basePdfPath;
        int iteration = 0;

        foreach (var src in sources)
        {
            // Temporary file to hold the result of the current insertion
            string tempOutput = $"temp_insert_{iteration}.pdf";

            // Determine the insertion position: after the last page of the current document
            int insertPosition;
            using (Document curDoc = new Document(currentFile))
            {
                // Pages are 1‑based; inserting at Count + 1 appends to the end
                insertPosition = curDoc.Pages.Count + 1;
            }

            // Perform the insertion
            bool success = editor.Insert(
                inputFile: currentFile,
                insertLocation: insertPosition,
                portFile: src.Path,
                startPage: src.StartPage,
                endPage: src.EndPage,
                outputFile: tempOutput);

            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from {src.Path} into {currentFile}");
                // Clean up temporary file if it was created
                if (File.Exists(tempOutput)) File.Delete(tempOutput);
                return;
            }

            // Delete the previous intermediate file (except the original base file)
            if (iteration > 0 && File.Exists(currentFile))
            {
                File.Delete(currentFile);
            }

            // Prepare for next iteration
            currentFile = tempOutput;
            iteration++;
        }

        // Rename the final intermediate file to the desired output name
        if (File.Exists(finalOutputPath))
        {
            File.Delete(finalOutputPath);
        }
        File.Move(currentFile, finalOutputPath);

        Console.WriteLine($"Batch insertion completed. Result saved to '{finalOutputPath}'.");
    }
}