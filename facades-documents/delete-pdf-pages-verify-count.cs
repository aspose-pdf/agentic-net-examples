using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Get page count before deletion.
            int beforeCount;
            using (Document srcDoc = new Document(inputPath))
            {
                beforeCount = srcDoc.Pages.Count;
            }

            // Perform deletion using PdfFileEditor (no using block as it is not IDisposable).
            var editor = new PdfFileEditor();
            bool deleteResult = editor.TryDelete(inputPath, pagesToDelete, outputPath);
            if (!deleteResult)
            {
                Console.Error.WriteLine("Delete operation failed.");
                return;
            }

            // Get page count after deletion.
            int afterCount;
            using (Document outDoc = new Document(outputPath))
            {
                afterCount = outDoc.Pages.Count;
            }

            // Validate the page count reduction.
            Console.WriteLine($"Pages before delete: {beforeCount}");
            Console.WriteLine($"Pages after  delete: {afterCount}");

            if (afterCount == beforeCount - pagesToDelete.Length)
            {
                Console.WriteLine("Delete operation reduced page count as expected.");
            }
            else
            {
                Console.WriteLine("Unexpected page count after delete.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}