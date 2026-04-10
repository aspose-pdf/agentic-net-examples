using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Get page count before deletion ----------
        PdfPageEditor pageEditorBefore = new PdfPageEditor();
        pageEditorBefore.BindPdf(inputPath);
        int beforeCount = pageEditorBefore.GetPages();

        // ---------- Perform delete operation ----------
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool deleteResult = fileEditor.TryDelete(inputPath, pagesToDelete, outputPath);
        if (!deleteResult)
        {
            Console.Error.WriteLine("Delete operation failed.");
            return;
        }

        // ---------- Get page count after deletion ----------
        PdfPageEditor pageEditorAfter = new PdfPageEditor();
        pageEditorAfter.BindPdf(outputPath);
        int afterCount = pageEditorAfter.GetPages();

        // ---------- Validate the reduction ----------
        Console.WriteLine($"Pages before delete: {beforeCount}");
        Console.WriteLine($"Pages after  delete: {afterCount}");

        if (afterCount < beforeCount)
        {
            Console.WriteLine("Validation succeeded: page count reduced.");
        }
        else
        {
            Console.WriteLine("Validation failed: page count not reduced.");
        }
    }
}