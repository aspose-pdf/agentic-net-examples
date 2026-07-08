using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        // Example: delete pages 2 and 3
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original page count
        int originalCount;
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPath);
            originalCount = viewer.PageCount;
        }

        // Delete specified pages
        PdfFileEditor editor = new PdfFileEditor();
        bool deleted = editor.TryDelete(inputPath, pagesToDelete, outputPath);
        if (!deleted)
        {
            Console.Error.WriteLine("Page deletion failed.");
            return;
        }

        // Get page count after deletion
        int newCount;
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(outputPath);
            newCount = viewer.PageCount;
        }

        // Validate that the page count decreased
        if (newCount < originalCount)
        {
            Console.WriteLine($"Success: page count reduced from {originalCount} to {newCount}.");
        }
        else
        {
            Console.WriteLine($"Failure: page count not reduced (original {originalCount}, after {newCount}).");
        }
    }
}