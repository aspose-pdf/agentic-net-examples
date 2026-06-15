using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string basePdf = "base.pdf";
        const string sourcePdf = "source.pdf";
        const string outputPdf = "output_concurrent.pdf";

        // Verify required files exist
        if (!File.Exists(basePdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine("Required PDF files not found.");
            return;
        }

        // Remove any previous output
        if (File.Exists(outputPdf))
            File.Delete(outputPdf);

        // Initialize output with a copy of the base PDF
        File.Copy(basePdf, outputPdf);

        // Number of concurrent import and delete operations
        int importTaskCount = 5;
        int deleteTaskCount = 5;

        // Synchronization object to serialize file access
        object fileLock = new object();

        // Create import tasks (append first page of sourcePdf)
        Task[] importTasks = new Task[importTaskCount];
        for (int i = 0; i < importTaskCount; i++)
        {
            importTasks[i] = Task.Run(() =>
            {
                try
                {
                    lock (fileLock)
                    {
                        PdfFileEditor editor = new PdfFileEditor();
                        // Append page 1 of sourcePdf to the end of outputPdf
                        // TryAppend returns false instead of throwing on failure
                        bool success = editor.TryAppend(outputPdf, new string[] { sourcePdf }, 1, 1, outputPdf);
                        Console.WriteLine($"Import completed: {success}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Import error: {ex.Message}");
                }
            });
        }

        // Create delete tasks (remove page 2 from outputPdf)
        Task[] deleteTasks = new Task[deleteTaskCount];
        for (int i = 0; i < deleteTaskCount; i++)
        {
            deleteTasks[i] = Task.Run(() =>
            {
                try
                {
                    lock (fileLock)
                    {
                        PdfFileEditor editor = new PdfFileEditor();
                        // Delete page 2 if it exists
                        bool success = editor.TryDelete(outputPdf, new int[] { 2 }, outputPdf);
                        Console.WriteLine($"Delete completed: {success}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Delete error: {ex.Message}");
                }
            });
        }

        // Wait for all operations to finish
        Task.WaitAll(importTasks);
        Task.WaitAll(deleteTasks);

        // Verify the resulting PDF can be opened and report page count
        try
        {
            using (Document finalDoc = new Document(outputPdf))
            {
                Console.WriteLine($"Final page count: {finalDoc.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }
}