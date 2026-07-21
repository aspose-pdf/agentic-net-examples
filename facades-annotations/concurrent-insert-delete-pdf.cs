using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class ConcurrentPdfTest
{
    static void Main()
    {
        // Paths to the PDFs used in the test.
        const string originalPdf = "original.pdf";   // PDF on which operations will be performed.
        const string sourcePdf   = "source.pdf";     // PDF whose pages will be inserted.
        const string importResult = "imported_result.pdf";
        const string deleteResult = "deleted_result.pdf";

        // Verify that the required files exist before starting the test.
        if (!File.Exists(originalPdf))
        {
            Console.Error.WriteLine($"Missing file: {originalPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Missing file: {sourcePdf}");
            return;
        }

        // Define two independent tasks: one inserts pages, the other deletes pages.
        Task insertTask = Task.Run(() =>
        {
            try
            {
                // Insert pages 1 and 2 from sourcePdf after page 1 of originalPdf.
                // The result is saved to importResult.
                PdfFileEditor editor = new PdfFileEditor();
                editor.Insert(originalPdf, 1, sourcePdf, new int[] { 1, 2 }, importResult);
                Console.WriteLine($"Insert operation completed: {importResult}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Insert task error: {ex.Message}");
            }
        });

        Task deleteTask = Task.Run(() =>
        {
            try
            {
                // Delete pages 2 and 3 from originalPdf.
                // The result is saved to deleteResult.
                PdfFileEditor editor = new PdfFileEditor();
                editor.Delete(originalPdf, new int[] { 2, 3 }, deleteResult);
                Console.WriteLine($"Delete operation completed: {deleteResult}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Delete task error: {ex.Message}");
            }
        });

        // Wait for both operations to finish.
        Task.WaitAll(insertTask, deleteTask);

        Console.WriteLine("Concurrent import and delete test finished.");
    }
}