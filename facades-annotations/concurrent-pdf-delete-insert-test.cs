using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class ConcurrentPdfTest
{
    // Paths to sample PDFs used for the test.
    // Ensure these files exist before running the test.
    private const string SourcePdf = "source.pdf";          // PDF on which operations will be performed
    private const string ImportPdf = "import.pdf";          // PDF whose pages will be imported
    private const string DeleteResultPdf = "delete_result.pdf";
    private const string ImportResultPdf = "import_result.pdf";

    static void Main()
    {
        // Verify that the required input files exist.
        if (!File.Exists(SourcePdf) || !File.Exists(ImportPdf))
        {
            Console.Error.WriteLine("Required input PDF files not found.");
            return;
        }

        // Run delete and import operations concurrently.
        Task<bool> deleteTask = Task.Run(() => DeletePages(SourcePdf, new[] { 2, 3 }, DeleteResultPdf));
        Task<bool> importTask = Task.Run(() => InsertPages(SourcePdf, ImportPdf, 1, ImportResultPdf));

        // Wait for both tasks to complete.
        Task.WaitAll(deleteTask, importTask);

        // Report results.
        Console.WriteLine($"Delete operation success: {deleteTask.Result}");
        Console.WriteLine($"Import operation success: {importTask.Result}");
    }

    // Deletes the specified pages from the input PDF and saves the result.
    // Returns true if the operation succeeded.
    private static bool DeletePages(string inputFile, int[] pagesToDelete, string outputFile)
    {
        try
        {
            // Each thread uses its own PdfFileEditor instance – no shared state.
            PdfFileEditor editor = new PdfFileEditor();

            // TryDelete does not throw; it returns a bool indicating success.
            bool result = editor.TryDelete(inputFile, pagesToDelete, outputFile);
            return result;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"DeletePages error: {ex.Message}");
            return false;
        }
    }

    // Inserts the first page of the import PDF after the specified page index of the source PDF.
    // Saves the resulting PDF to outputFile and returns true on success.
    private static bool InsertPages(string sourceFile, string importFile, int insertAfterPage, string outputFile)
    {
        try
        {
            // Each thread uses its own PdfFileEditor instance.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the first page (page number 1) of importFile after insertAfterPage of sourceFile.
            // The Insert method works with file paths.
            editor.Insert(sourceFile, insertAfterPage, importFile, new[] { 1 }, outputFile);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"InsertPages error: {ex.Message}");
            return false;
        }
    }
}