using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API (for page count)
using Aspose.Pdf.Facades;        // Facade API (PdfFileEditor)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Determine total page count to build left (odd) and right (even) page arrays
        // ------------------------------------------------------------
        int totalPages;
        using (FileStream countStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Document doc = new Document(countStream))   // Document implements IDisposable
        {
            totalPages = doc.Pages.Count;                  // 1‑based page indexing
        }

        // Build arrays: left pages = odd numbers, right pages = even numbers
        int oddCount  = (totalPages + 1) / 2;
        int evenCount = totalPages / 2;

        int[] leftPages  = new int[oddCount];
        int[] rightPages = new int[evenCount];

        int leftIdx = 0, rightIdx = 0;
        for (int i = 1; i <= totalPages; i++)               // iterate 1‑based
        {
            if ((i % 2) == 1)                               // odd page → left side
                leftPages[leftIdx++] = i;
            else                                            // even page → right side
                rightPages[rightIdx++] = i;
        }

        // ------------------------------------------------------------
        // Create the booklet using PdfFileEditor (no using – not IDisposable)
        // ------------------------------------------------------------
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);
            Console.WriteLine(success ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}