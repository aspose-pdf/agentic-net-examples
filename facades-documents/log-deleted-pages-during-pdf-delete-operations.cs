using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Verify input exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the delete operations (each array contains page numbers to delete)
        List<int[]> deleteOperations = new List<int[]>
        {
            new int[] { 2, 3 },   // delete pages 2 and 3
            new int[] { 1 }       // delete page 1 (after previous deletion)
        };

        // Working file path – starts with the original input
        string currentPath = inputPath;
        int operationIndex = 1;

        foreach (int[] pagesToDelete in deleteOperations)
        {
            // Log intended deletion
            Console.WriteLine($"Operation {operationIndex}: Request to delete {pagesToDelete.Length} page(s) [{string.Join(", ", pagesToDelete)}]");

            // Capture page count before deletion
            int beforeCount;
            using (Document docBefore = new Document(currentPath))
            {
                beforeCount = docBefore.Pages.Count;
            }

            // Prepare temporary output file for this operation
            string tempOutput = $"temp_deleted_{operationIndex}.pdf";

            // Perform deletion using PdfFileEditor (Facades API)
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.TryDelete(currentPath, pagesToDelete, tempOutput);

            if (!success)
            {
                Console.Error.WriteLine($"Operation {operationIndex}: Delete failed.");
                break;
            }

            // Capture page count after deletion
            int afterCount;
            using (Document docAfter = new Document(tempOutput))
            {
                afterCount = docAfter.Pages.Count;
            }

            // Compute and log the actual number of pages removed
            int removedPages = beforeCount - afterCount;
            Console.WriteLine($"Operation {operationIndex}: Successfully removed {removedPages} page(s).");

            // Prepare for the next iteration
            currentPath = tempOutput;
            operationIndex++;
        }

        // Final output file name
        const string finalOutput = "output_deleted.pdf";

        // Move the last temporary file to the final output name
        try
        {
            if (File.Exists(finalOutput))
                File.Delete(finalOutput);
            File.Move(currentPath, finalOutput);
            Console.WriteLine($"All delete operations completed. Final PDF saved as '{finalOutput}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error moving final file: {ex.Message}");
        }
    }
}