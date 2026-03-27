using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class BookletGenerator
{
    public static void CreateBookletFromSecondHalfRightPages(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        int totalPages;
        using (Document doc = new Document(inputPath))
        {
            totalPages = doc.Pages.Count;
        }

        int half = totalPages / 2;
        int rightCount = totalPages - half;
        int[] rightPages = new int[rightCount];
        for (int i = 0; i < rightCount; i++)
        {
            rightPages[i] = half + 1 + i; // pages from second half
        }

        int[] leftPages = new int[0]; // no left pages

        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
        if (success)
        {
            Console.WriteLine($"Booklet created: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }

    public static void Main()
    {
        const string inputFile = "input.pdf";
        const string outputFile = "booklet.pdf";
        CreateBookletFromSecondHalfRightPages(inputFile, outputFile);
    }
}