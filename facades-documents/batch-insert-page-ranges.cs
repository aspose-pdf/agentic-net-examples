using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Source PDF files and the page ranges to insert from each.
        string[] sourceFiles = { "source1.pdf", "source2.pdf", "source3.pdf" };
        int[] startPages   = { 1, 2, 5 };
        int[] endPages     = { 3, 4, 7 };

        // Destination PDF that will contain all inserted pages.
        const string destinationFile = "merged.pdf";

        // Initialise the destination by copying the first source PDF.
        if (!File.Exists(sourceFiles[0]))
        {
            Console.Error.WriteLine($"Source file not found: {sourceFiles[0]}");
            return;
        }
        File.Copy(sourceFiles[0], destinationFile, true);

        // Loop through the remaining source PDFs and insert their page ranges.
        for (int i = 1; i < sourceFiles.Length; i++)
        {
            string src = sourceFiles[i];
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Source file not found: {src}");
                continue;
            }

            // Determine the insertion position – after the last page of the current destination.
            int insertPos;
            using (Document destDoc = new Document(destinationFile))
            {
                // Pages are 1‑based; inserting after the last page means position = pageCount + 1.
                insertPos = destDoc.Pages.Count + 1;
            }

            // Perform the insertion using PdfFileEditor.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(
                destinationFile,   // inputFile (current destination)
                insertPos,         // insertLocation
                src,               // portFile (source PDF)
                startPages[i],     // startPage in source
                endPages[i],       // endPage in source
                destinationFile);  // outputFile (overwrite destination)

            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from {src}");
            }
        }

        Console.WriteLine($"Batch insertion completed. Result saved to '{destinationFile}'.");
    }
}