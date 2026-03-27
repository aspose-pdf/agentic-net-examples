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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Determine the total number of pages in the source PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build left (odd) and right (even) page arrays
        int leftArrayLength = (pageCount + 1) / 2;
        int rightArrayLength = pageCount / 2;
        int[] leftPages = new int[leftArrayLength];
        int[] rightPages = new int[rightArrayLength];
        int leftIndex = 0;
        int rightIndex = 0;
        int pageIndex = 1;
        while (pageIndex <= pageCount)
        {
            if (pageIndex % 2 == 1)
            {
                leftPages[leftIndex] = pageIndex;
                leftIndex++;
            }
            else
            {
                rightPages[rightIndex] = pageIndex;
                rightIndex++;
            }
            pageIndex++;
        }

        // Create streams and generate the booklet
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);
            Console.WriteLine(success ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}