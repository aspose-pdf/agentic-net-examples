using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ConcurrentPdfTest
{
    // Paths for test files
    private const string BasePdfPath = "base.pdf";
    private const string ExtraPdfPath = "extra.pdf";
    private const string ResultPdfPath = "result.pdf";

    static void Main()
    {
        // Ensure clean state
        CleanupFiles();

        // Create base PDF (3 pages)
        using (Document baseDoc = new Document())
        {
            for (int i = 0; i < 3; i++)
                baseDoc.Pages.Add();

            baseDoc.Save(BasePdfPath);
        }

        // Create extra PDF (2 pages) to be imported
        using (Document extraDoc = new Document())
        {
            for (int i = 0; i < 2; i++)
                extraDoc.Pages.Add();

            extraDoc.Save(ExtraPdfPath);
        }

        // Prepare threads
        Thread importThread = new Thread(ImportPages);
        Thread deleteThread = new Thread(DeletePages);

        // Start both operations concurrently
        importThread.Start();
        deleteThread.Start();

        // Wait for both to finish
        importThread.Join();
        deleteThread.Join();

        // Verify result
        if (File.Exists(ResultPdfPath))
        {
            using (Document resultDoc = new Document(ResultPdfPath))
            {
                Console.WriteLine($"Result PDF created with {resultDoc.Pages.Count} pages.");
            }
        }
        else
        {
            Console.WriteLine("Result PDF was not created.");
        }

        // Clean up temporary files
        CleanupFiles();
    }

    // Appends ExtraPdfPath to BasePdfPath and writes to ResultPdfPath
    private static void ImportPages()
    {
        try
        {
            // Load both documents
            using (Document baseDoc = new Document(BasePdfPath))
            using (Document extraDoc = new Document(ExtraPdfPath))
            {
                // Append all pages from extraDoc to the end of baseDoc
                baseDoc.Pages.Insert(baseDoc.Pages.Count + 1, extraDoc.Pages);
                // Save the combined document
                baseDoc.Save(ResultPdfPath);
            }
            Console.WriteLine("Import operation completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Import error: {ex.Message}");
        }
    }

    // Deletes page 2 from BasePdfPath and writes to ResultPdfPath
    private static void DeletePages()
    {
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Delete page number 2 (1‑based indexing) from base.pdf, output to result.pdf
            bool success = editor.TryDelete(BasePdfPath, new int[] { 2 }, ResultPdfPath);
            Console.WriteLine($"Delete operation {(success ? "succeeded" : "failed")}." );
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Delete error: {ex.Message}");
        }
    }

    // Helper to remove test files before/after execution
    private static void CleanupFiles()
    {
        foreach (var path in new[] { BasePdfPath, ExtraPdfPath, ResultPdfPath })
        {
            try { if (File.Exists(path)) File.Delete(path); } catch { }
        }
    }
}
