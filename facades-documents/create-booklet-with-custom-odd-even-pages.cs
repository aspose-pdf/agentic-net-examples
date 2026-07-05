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

        // Determine total number of pages in the source PDF
        int totalPages;
        using (Document doc = new Document(inputPath))
        {
            totalPages = doc.Pages.Count;
        }

        // Build page lists: left side (odd pages), right side (even pages)
        List<int> leftPagesList = new List<int>();
        List<int> rightPagesList = new List<int>();
        for (int i = 1; i <= totalPages; i++)
        {
            if (i % 2 == 1) // odd page number
                leftPagesList.Add(i);
            else // even page number
                rightPagesList.Add(i);
        }

        int[] leftPages = leftPagesList.ToArray();
        int[] rightPages = rightPagesList.ToArray();

        // Create the PdfFileEditor and generate the customized booklet
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        if (result)
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}