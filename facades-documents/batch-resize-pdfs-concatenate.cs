using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchResizeAndConcat
{
    static void Main(string[] args)
    {
        // Input PDF files – can be passed as command‑line arguments or defined here.
        // For this example we expect the file paths in the args array.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Please provide at least one PDF file path as an argument.");
            return;
        }

        // Output file that will contain all resized PDFs concatenated together.
        const string outputFile = "merged_resized.pdf";

        // Temporary folder for intermediate resized PDFs.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfResizeTmp");
        Directory.CreateDirectory(tempFolder);

        var resizedFiles = new List<string>();

        try
        {
            // -----------------------------------------------------------------
            // Step 1 – Resize each input PDF to 1024 x 768 points.
            // -----------------------------------------------------------------
            foreach (string inputPath in args)
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    continue;
                }

                // Determine the number of pages in the source PDF.
                int pageCount;
                using (Document srcDoc = new Document(inputPath))
                {
                    pageCount = srcDoc.Pages.Count; // 1‑based indexing
                }

                // Build an array with all page numbers (1‑based).
                int[] allPages = Enumerable.Range(1, pageCount).ToArray();

                // Create a temporary file name for the resized PDF.
                string tempPath = Path.Combine(tempFolder, Guid.NewGuid().ToString() + ".pdf");

                // Use PdfFileEditor to resize the contents of each page.
                // The overload ResizeContents(string, string, int[], double, double)
                // shrinks the page contents to the specified width and height (in points).
                PdfFileEditor editor = new PdfFileEditor();
                editor.ResizeContents(inputPath, tempPath, allPages, 1024, 768);

                resizedFiles.Add(tempPath);
                Console.WriteLine($"Resized '{inputPath}' → '{tempPath}'");
            }

            if (resizedFiles.Count == 0)
            {
                Console.Error.WriteLine("No PDFs were successfully resized. Aborting concatenation.");
                return;
            }

            // -----------------------------------------------------------------
            // Step 2 – Concatenate all resized PDFs into a single document.
            // -----------------------------------------------------------------
            PdfFileEditor concatEditor = new PdfFileEditor();
            bool success = concatEditor.Concatenate(resizedFiles.ToArray(), outputFile);

            if (success)
                Console.WriteLine($"All resized PDFs concatenated into '{outputFile}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        finally
        {
            // Clean up temporary resized files.
            foreach (string file in resizedFiles)
            {
                try { File.Delete(file); } catch { /* ignore */ }
            }

            // Optionally remove the temporary folder if it is empty.
            try
            {
                if (Directory.Exists(tempFolder) && !Directory.EnumerateFileSystemEntries(tempFolder).Any())
                    Directory.Delete(tempFolder);
            }
            catch { /* ignore */ }
        }
    }
}