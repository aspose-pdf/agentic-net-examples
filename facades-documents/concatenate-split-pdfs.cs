using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Resolve the folder that should contain the split PDFs.
        // Using the application base directory makes the sample runnable out‑of‑the‑box on any machine.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string splitFolder = Path.Combine(baseDir, "SplitResults");

        // If the folder does not exist, create it (or you could abort with a clear message).
        if (!Directory.Exists(splitFolder))
        {
            Console.Error.WriteLine($"Folder '{splitFolder}' does not exist. Creating an empty folder.");
            Directory.CreateDirectory(splitFolder);
        }

        // Final merged PDF file name – stored next to the executable for simplicity.
        const string mergedPdfFileName = "merged_output.pdf";
        string mergedPdfPath = Path.Combine(baseDir, mergedPdfFileName);

        // Get all PDF files in the folder, sorted to preserve original order.
        string[] splitFiles = Directory.GetFiles(splitFolder, "*.pdf");
        Array.Sort(splitFiles, StringComparer.OrdinalIgnoreCase);

        if (splitFiles.Length == 0)
        {
            Console.Error.WriteLine($"No PDF files found in folder '{splitFolder}'. Nothing to merge.");
            return;
        }

        // If there is only one file, just copy it to the target name.
        if (splitFiles.Length == 1)
        {
            File.Copy(splitFiles[0], mergedPdfPath, overwrite: true);
            Console.WriteLine($"Only one split PDF found. Copied to '{mergedPdfPath}'.");
            return;
        }

        // The first file will serve as the initial document to which others are inserted.
        string currentFile = splitFiles[0];
        PdfFileEditor editor = new PdfFileEditor();

        for (int i = 1; i < splitFiles.Length; i++)
        {
            string nextFile = splitFiles[i];

            // Determine the number of pages in the file that will be inserted.
            int pagesToInsert;
            using (Document docNext = new Document(nextFile))
            {
                pagesToInsert = docNext.Pages.Count;
            }

            // Determine the insertion position – after the last page of the current document.
            int insertLocation;
            using (Document docCurrent = new Document(currentFile))
            {
                insertLocation = docCurrent.Pages.Count + 1; // 1‑based indexing
            }

            // Decide the output path for this iteration.
            // The final iteration writes directly to the desired merged file.
            string outputPath = (i == splitFiles.Length - 1)
                ? mergedPdfPath
                : Path.Combine(Path.GetTempPath(), $"temp_merge_{i}.pdf");

            // Perform the insertion using PdfFileEditor.Insert (range overload).
            editor.Insert(currentFile, insertLocation, nextFile, 1, pagesToInsert, outputPath);

            // Clean up the temporary file created in the previous iteration (if any).
            if (i > 1 && File.Exists(currentFile) && !currentFile.Equals(splitFiles[0], StringComparison.OrdinalIgnoreCase))
            {
                try { File.Delete(currentFile); } catch { /* ignore cleanup errors */ }
            }

            // The output of this step becomes the input for the next iteration.
            currentFile = outputPath;
        }

        Console.WriteLine($"All split PDFs have been concatenated into '{mergedPdfPath}'.");
    }
}
