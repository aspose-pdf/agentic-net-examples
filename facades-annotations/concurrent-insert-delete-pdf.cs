using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Paths to the original PDFs
    private const string SourcePdfPath = "source.pdf";      // PDF that will be modified concurrently
    private const string ImportPdfPath = "import.pdf";      // PDF whose pages will be inserted
    private const string ResultPdfPath = "result.pdf";      // Final output after all operations

    // Lock object to synchronize access to the working PDF file
    private static readonly object _fileLock = new object();

    static void Main()
    {
        // Ensure the source PDF exists
        if (!File.Exists(SourcePdfPath) || !File.Exists(ImportPdfPath))
        {
            Console.Error.WriteLine("Required PDF files not found.");
            return;
        }

        // Create a working copy of the source PDF that will be accessed by all threads
        string workingPdfPath = Path.Combine(Path.GetTempPath(), $"working_{Guid.NewGuid()}.pdf");
        File.Copy(SourcePdfPath, workingPdfPath, true);

        // Number of concurrent operations per thread
        const int operationsPerThread = 5;

        // Create tasks: one for inserting pages, one for deleting pages
        Task insertTask = Task.Run(() => InsertPagesConcurrently(workingPdfPath, operationsPerThread));
        Task deleteTask = Task.Run(() => DeletePagesConcurrently(workingPdfPath, operationsPerThread));

        // Wait for both tasks to finish
        Task.WaitAll(insertTask, deleteTask);

        // Move the final working PDF to the result path
        if (File.Exists(ResultPdfPath))
            File.Delete(ResultPdfPath);
        File.Move(workingPdfPath, ResultPdfPath);

        // Verify the final document (e.g., page count) using Aspose.Pdf.Document
        using (Aspose.Pdf.Document finalDoc = new Aspose.Pdf.Document(ResultPdfPath))
        {
            Console.WriteLine($"Final PDF saved to '{ResultPdfPath}'. Page count: {finalDoc.Pages.Count}");
        }
    }

    // Inserts pages from ImportPdfPath into the target PDF repeatedly
    private static void InsertPagesConcurrently(string targetPath, int repetitions)
    {
        PdfFileEditor editor = new PdfFileEditor();

        for (int i = 0; i < repetitions; i++)
        {
            // Determine a random insertion position (1‑based index)
            int insertPosition;
            lock (_fileLock)
            {
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(targetPath))
                {
                    insertPosition = new Random().Next(1, doc.Pages.Count + 1);
                }
            }

            // Insert the first page of the import PDF at the chosen position
            // Use TryInsert to avoid exceptions propagating from race conditions
            string tempOutput = Path.GetTempFileName();
            bool success = false;
            lock (_fileLock)
            {
                success = editor.TryInsert(
                    targetPath,                     // input PDF
                    insertPosition,                 // position where pages will be inserted
                    ImportPdfPath,                  // source PDF
                    new int[] { 1 },                // pages to insert from source (1‑based)
                    tempOutput);                    // output PDF
                if (success)
                {
                    // Replace the original file with the newly created one
                    File.Delete(targetPath);
                    File.Move(tempOutput, targetPath);
                }
                else
                {
                    // Cleanup temporary file if operation failed
                    File.Delete(tempOutput);
                }
            }

            // Small pause to increase interleaving chance
            Thread.Sleep(50);
        }
    }

    // Deletes random pages from the target PDF repeatedly
    private static void DeletePagesConcurrently(string targetPath, int repetitions)
    {
        PdfFileEditor editor = new PdfFileEditor();

        for (int i = 0; i < repetitions; i++)
        {
            int[] pagesToDelete = null;
            lock (_fileLock)
            {
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(targetPath))
                {
                    if (doc.Pages.Count == 0)
                        continue; // nothing to delete

                    // Choose up to two random pages to delete
                    Random rnd = new Random();
                    int deleteCount = Math.Min(2, doc.Pages.Count);
                    pagesToDelete = new int[deleteCount];
                    for (int j = 0; j < deleteCount; j++)
                    {
                        pagesToDelete[j] = rnd.Next(1, doc.Pages.Count + 1); // 1‑based index
                    }
                }
            }

            // Perform the delete operation using TryDelete
            string tempOutput = Path.GetTempFileName();
            bool success = false;
            lock (_fileLock)
            {
                success = editor.TryDelete(
                    targetPath,          // input PDF
                    pagesToDelete,       // pages to delete (1‑based)
                    tempOutput);         // output PDF

                if (success)
                {
                    File.Delete(targetPath);
                    File.Move(tempOutput, targetPath);
                }
                else
                {
                    File.Delete(tempOutput);
                }
            }

            // Small pause to increase interleaving chance
            Thread.Sleep(50);
        }
    }
}