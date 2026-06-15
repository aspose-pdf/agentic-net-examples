using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "booklet_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Determine total page count using Document (must be disposed)
        int totalPages;
        using (Document doc = new Document(inputPdf))
        {
            totalPages = doc.Pages.Count;
        }

        // Build left (odd) and right (even) page sequences
        int leftCount  = (totalPages + 1) / 2; // odd pages count
        int rightCount = totalPages / 2;       // even pages count

        int[] leftPages  = new int[leftCount];
        int[] rightPages = new int[rightCount];

        // Fill arrays: Aspose.Pdf uses 1‑based page indexing
        for (int i = 0, odd = 1, even = 2; i < leftCount; i++, odd += 2)
            leftPages[i] = odd;

        for (int i = 0, even = 2; i < rightCount; i++, even += 2)
            rightPages[i] = even;

        // Create booklet with custom page ordering
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPdf, outputPdf, leftPages, rightPages);

        Console.WriteLine(success
            ? $"Booklet created successfully: {outputPdf}"
            : "Failed to create booklet.");
    }
}