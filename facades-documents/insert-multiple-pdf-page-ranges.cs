using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base PDF into which pages will be inserted
        const string basePdfPath = "base.pdf";

        // Output PDF after all insertions
        const string finalOutputPath = "merged.pdf";

        // Array of source PDFs and their page ranges (start, end)
        // Example: insert pages 2‑4 from source1.pdf and pages 1‑2 from source2.pdf
        var sources = new (string Path, int StartPage, int EndPage)[]
        {
            ("source1.pdf", 2, 4),
            ("source2.pdf", 1, 2)
        };

        // Validate files exist
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base file not found: {basePdfPath}");
            return;
        }

        foreach (var src in sources)
        {
            if (!File.Exists(src.Path))
            {
                Console.Error.WriteLine($"Source file not found: {src.Path}");
                return;
            }
        }

        // Working file that will be updated after each insertion
        string workingFile = basePdfPath;

        // Insert each source PDF sequentially
        for (int i = 0; i < sources.Length; i++)
        {
            var src = sources[i];

            // Temporary file to hold the result of the current insertion
            string tempOutput = Path.GetTempFileName();

            // Insert location – 1 inserts before the first page.
            // Adjust as needed (e.g., use the page count + 1 to append at the end).
            int insertLocation = 1;

            // Perform the insertion using the range overload
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(
                workingFile,          // input PDF (current state)
                insertLocation,       // position where pages will be inserted
                src.Path,             // source PDF
                src.StartPage,        // start page in source PDF
                src.EndPage,          // end page in source PDF
                tempOutput);          // output PDF after insertion

            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from {src.Path}");
                return;
            }

            // The temporary output becomes the new working file for the next iteration
            workingFile = tempOutput;
        }

        // Move the final result to the desired output location
        File.Copy(workingFile, finalOutputPath, overwrite: true);
        Console.WriteLine($"Pages inserted successfully. Result saved to '{finalOutputPath}'.");
    }
}