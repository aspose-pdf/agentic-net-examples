using System;
using System.IO;
using Aspose.Pdf;                 // Core API for reading page count
using Aspose.Pdf.Facades;        // Facade API for page deletion

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Determine the last page number using the core Document API
            int lastPage;
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                lastPage = doc.Pages.Count;   // 1‑based indexing
            }

            // Prepare an integer array with the first and last page numbers
            int[] pagesToDelete = new int[] { 1, lastPage };

            // Use the Facade PdfFileEditor to delete the specified pages
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"First and last pages removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}