using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths of the split PDF files that need to be concatenated.
        // Adjust these paths as necessary.
        string[] splitFiles = new string[]
        {
            "split_part_1.pdf",
            "split_part_2.pdf",
            "split_part_3.pdf"
        };

        // Final merged PDF file.
        const string mergedOutput = "merged_result.pdf";

        // Validate input.
        foreach (var file in splitFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Start with the first split file as the base document.
        string currentBase = splitFiles[0];

        // Iterate over the remaining split files and insert them sequentially.
        for (int i = 1; i < splitFiles.Length; i++)
        {
            string fileToInsert = splitFiles[i];
            string tempResult = Path.GetTempFileName() + ".pdf";

            // Determine the insertion position: after the last page of the current base.
            int insertPosition;
            using (Document baseDoc = new Document(currentBase))
            {
                insertPosition = baseDoc.Pages.Count + 1; // 1‑based indexing
            }

            // Determine the range of pages to insert from the source file (all pages).
            int endPage;
            using (Document srcDoc = new Document(fileToInsert))
            {
                endPage = srcDoc.Pages.Count;
            }

            // Perform the insertion using PdfFileEditor.
            PdfFileEditor editor = new PdfFileEditor();
            editor.Insert(currentBase, insertPosition, fileToInsert, 1, endPage, tempResult);

            // Clean up the previous temporary base file (if it was generated in a prior iteration).
            if (i > 1 && File.Exists(currentBase))
            {
                File.Delete(currentBase);
            }

            // The newly created file becomes the base for the next iteration.
            currentBase = tempResult;
        }

        // Copy the final temporary file to the desired output location.
        File.Copy(currentBase, mergedOutput, true);

        // Remove the last temporary file if it is not the original first split file.
        if (currentBase != splitFiles[0] && File.Exists(currentBase))
        {
            File.Delete(currentBase);
        }

        Console.WriteLine($"Merged PDF created at: {mergedOutput}");
    }
}