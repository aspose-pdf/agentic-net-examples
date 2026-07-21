using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;               // Document class for page count
using Aspose.Pdf.Facades;      // PdfFileEditor for booklet creation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "booklet.pdf"; // resulting booklet PDF

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Determine the total number of pages in the source document.
        // Document implements IDisposable, so wrap it in a using block.
        // ------------------------------------------------------------
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // Aspose.Pdf uses 1‑based indexing
        }

        // ------------------------------------------------------------
        // Build the left‑hand (even) and right‑hand (odd) page arrays.
        // In a booklet the first spread has page 1 on the right side
        // and page 2 on the left side, then 3 right, 4 left, etc.
        // ------------------------------------------------------------
        List<int> leftPagesList  = new List<int>(); // even page numbers
        List<int> rightPagesList = new List<int>(); // odd page numbers

        for (int i = 1; i <= pageCount; i++)
        {
            if (i % 2 == 0)   // even → left side
                leftPagesList.Add(i);
            else               // odd → right side
                rightPagesList.Add(i);
        }

        int[] leftPages  = leftPagesList.ToArray();
        int[] rightPages = rightPagesList.ToArray();

        // ------------------------------------------------------------
        // Create the booklet using the custom page order.
        // PdfFileEditor does NOT implement IDisposable, so no using block.
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        // Report the outcome
        Console.WriteLine(result
            ? $"Booklet created successfully: {outputPath}"
            : "Failed to create booklet.");
    }
}