using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example: delete pages 2 and 4 using the core Document API
        int[] pagesToDelete = new int[] { 2, 4 };
        DeletePagesWithLogging(inputPath, pagesToDelete, outputPath);

        // Example: delete pages using the Facade API (optional)
        // DeletePagesWithFacade(inputPath, pagesToDelete, "output_facade.pdf");
    }

    // Core API deletion with audit logging
    static void DeletePagesWithLogging(string sourceFile, int[] pages, string destFile)
    {
        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(sourceFile))
        {
            // Log the number of pages that will be removed
            Console.WriteLine($"Attempting to delete {pages.Length} page(s): {string.Join(", ", pages)}");

            // Perform the deletion
            doc.Pages.Delete(pages);

            // Log the remaining page count after deletion
            Console.WriteLine($"Deletion completed. Remaining pages: {doc.Pages.Count}");

            // Save the modified document (save rule)
            doc.Save(destFile);
        }

        Console.WriteLine($"Document saved to '{destFile}'.");
    }

    // Facade API deletion with audit logging (optional alternative)
    static void DeletePagesWithFacade(string sourceFile, int[] pages, string destFile)
    {
        // Log the number of pages that will be removed
        Console.WriteLine($"Facade: attempting to delete {pages.Length} page(s) from '{sourceFile}'.");

        // Use PdfFileEditor which works directly on files
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.TryDelete(sourceFile, pages, destFile);

        if (success)
        {
            Console.WriteLine($"Facade deletion succeeded. Deleted {pages.Length} page(s). Output saved to '{destFile}'.");
        }
        else
        {
            Console.WriteLine("Facade deletion failed.");
        }
    }
}