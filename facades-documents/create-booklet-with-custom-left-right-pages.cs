using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get total number of pages in the source PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build left‑hand (odd) and right‑hand (even) page arrays
        List<int> leftPagesList = new List<int>();
        List<int> rightPagesList = new List<int>();
        for (int i = 1; i <= pageCount; i++)
        {
            if (i % 2 == 1) // odd page number
                leftPagesList.Add(i);
            else // even page number
                rightPagesList.Add(i);
        }

        int[] leftPages = leftPagesList.ToArray();
        int[] rightPages = rightPagesList.ToArray();

        // Create the booklet using the custom page order
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        Console.WriteLine(result
            ? $"Booklet created successfully: {outputPath}"
            : "Failed to create booklet.");
    }
}