using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class ConcurrentPdfTest
{
    // Paths to the PDFs used in the test
    private const string BasePdfPath = "base.pdf";          // The PDF on which operations are performed
    private const string InsertPdfPath = "insert.pdf";      // PDF whose pages will be inserted
    private const string OutputFolder = "ConcurrentOutputs";

    // Lock object to synchronize access to the PDF file
    private static readonly object _pdfLock = new object();

    static void Main()
    {
        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Verify that the required PDFs exist
        if (!File.Exists(BasePdfPath) || !File.Exists(InsertPdfPath))
        {
            Console.Error.WriteLine("Required PDF files not found. Ensure 'base.pdf' and 'insert.pdf' exist.");
            return;
        }

        // Number of concurrent tasks for each operation
        const int importTaskCount = 5;
        const int deleteTaskCount = 5;

        // Create and start import tasks
        Task[] importTasks = new Task[importTaskCount];
        for (int i = 0; i < importTaskCount; i++)
        {
            int taskId = i; // capture loop variable
            importTasks[i] = Task.Run(() => ImportPageTask(taskId));
        }

        // Create and start delete tasks
        Task[] deleteTasks = new Task[deleteTaskCount];
        for (int i = 0; i < deleteTaskCount; i++)
        {
            int taskId = i; // capture loop variable
            deleteTasks[i] = Task.Run(() => DeletePageTask(taskId));
        }

        // Wait for all tasks to complete
        Task.WaitAll(importTasks);
        Task.WaitAll(deleteTasks);

        Console.WriteLine("Concurrent import and delete operations completed.");
    }

    // Inserts the first page of InsertPdfPath into BasePdfPath at position 1
    private static void ImportPageTask(int taskId)
    {
        string outputPath = Path.Combine(OutputFolder, $"import_result_{taskId}.pdf");

        // Synchronize the operation to avoid race conditions on the same source file
        lock (_pdfLock)
        {
            // PdfFileEditor does NOT implement IDisposable; no using block needed
            PdfFileEditor editor = new PdfFileEditor();

            // Correct overload: Insert(sourceFile, startPageNumber, outputFile, insertPageNumbers, insertFile)
            editor.Insert(BasePdfPath, 1, outputPath, new int[] { 1 }, InsertPdfPath);
        }

        Console.WriteLine($"Import task {taskId} finished. Output: {outputPath}");
    }

    // Deletes page 2 from BasePdfPath and writes the result to a new file
    private static void DeletePageTask(int taskId)
    {
        string outputPath = Path.Combine(OutputFolder, $"delete_result_{taskId}.pdf");

        // Synchronize the operation to avoid race conditions on the same source file
        lock (_pdfLock)
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Correct overload: Delete(sourceFile, pageNumbers, outputFile)
            editor.Delete(BasePdfPath, new int[] { 2 }, outputPath);
        }

        Console.WriteLine($"Delete task {taskId} finished. Output: {outputPath}");
    }
}
