using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the split PDF files (e.g., Page1.pdf, Page2.pdf, ...)
        const string splitFolder = "SplitPages";
        // Final merged PDF file
        const string mergedPdf = "merged.pdf";

        // Verify that the folder exists; if not, report the problem and exit gracefully.
        if (!Directory.Exists(splitFolder))
        {
            Console.Error.WriteLine($"The folder '{splitFolder}' does not exist. Please ensure the split PDF files are placed there.");
            return;
        }

        // Get all PDF files in the folder, sorted alphabetically (adjust sorting if needed)
        string[] splitFiles = Directory.GetFiles(splitFolder, "*.pdf")
                                       .OrderBy(f => f)
                                       .ToArray();

        if (splitFiles.Length == 0)
        {
            Console.Error.WriteLine("No split PDF files found.");
            return;
        }

        // Start with the first split file as the initial merged document
        File.Copy(splitFiles[0], mergedPdf, true);
        string currentMerged = mergedPdf;

        // Helper instance of PdfFileEditor (does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Insert each subsequent split file at the end of the current merged document
        for (int i = 1; i < splitFiles.Length; i++)
        {
            string sourceFile = splitFiles[i];

            // Determine the insert position: after the last page of the current merged PDF
            int insertPosition;
            using (Document mergedDoc = new Document(currentMerged))
            {
                insertPosition = mergedDoc.Pages.Count + 1; // 1‑based indexing
            }

            // Determine the page range of the source file (insert all its pages)
            int startPage = 1;
            int endPage;
            using (Document srcDoc = new Document(sourceFile))
            {
                endPage = srcDoc.Pages.Count;
            }

            // Temporary file to hold the result of this insertion step
            string tempOutput = Path.GetTempFileName();

            // Perform the insertion
            bool success = editor.Insert(
                currentMerged,   // inputFile (current merged PDF)
                insertPosition, // insertLocation (after last page)
                sourceFile,     // portFile (PDF to insert)
                startPage,      // startPage of source
                endPage,        // endPage of source
                tempOutput);    // outputFile

            if (!success)
            {
                Console.Error.WriteLine($"Failed to insert pages from '{sourceFile}'.");
                // Clean up temporary file and abort
                File.Delete(tempOutput);
                return;
            }

            // Replace the current merged file with the newly created one
            File.Delete(currentMerged);
            File.Move(tempOutput, currentMerged);
        }

        Console.WriteLine($"All split PDFs have been concatenated into '{mergedPdf}'.");
    }
}
