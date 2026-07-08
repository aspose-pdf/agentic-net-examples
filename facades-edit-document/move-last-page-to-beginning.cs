using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string intermediatePath = "intermediate.pdf";
        const string outputPath = "reordered.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Determine the original page count (1‑based indexing)
        int originalPageCount;
        using (Document srcDoc = new Document(inputPath))
        {
            originalPageCount = srcDoc.Pages.Count;
        }

        // -----------------------------------------------------------------
        // Step 1: Insert the last page (originalPageCount) at the beginning
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        // InsertLocation = 1 means the new page will become the first page.
        // The port file is the same as the source file; we copy only the last page.
        editor.Insert(
            inputPath,                // inputFile
            1,                        // insertLocation (before page 1)
            inputPath,                // portFile (source of pages to insert)
            originalPageCount,        // startPage (last page)
            originalPageCount,        // endPage   (last page)
            intermediatePath);        // outputFile (temporary result)

        // -----------------------------------------------------------------
        // Step 2: Delete the original last page, which is now the last page
        //         of the intermediate document (position originalPageCount + 1)
        // -----------------------------------------------------------------
        int pageToDelete = originalPageCount + 1; // the duplicated last page at the end
        editor.Delete(
            intermediatePath,                 // inputFile
            new int[] { pageToDelete },       // pages to delete
            outputPath);                      // final output file

        // Optional cleanup of the intermediate file
        try { File.Delete(intermediatePath); } catch { /* ignore */ }

        Console.WriteLine($"Pages reordered: last page moved to the beginning. Output saved to '{outputPath}'.");
    }
}