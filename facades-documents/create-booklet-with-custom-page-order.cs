using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "booklet.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source document to determine the total number of pages.
            Document srcDoc = new Document(inputPdf);
            int pageCount = srcDoc.Pages.Count;

            // Build page‑number arrays: odd pages on the left, even pages on the right.
            List<int> leftHandOddPages = new List<int>();
            List<int> rightHandEvenPages = new List<int>();

            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 1)
                    leftHandOddPages.Add(i);   // odd page → left side
                else
                    rightHandEvenPages.Add(i); // even page → right side
            }

            PdfFileEditor editor = new PdfFileEditor();
            // The overload expects int[] for left‑hand odd pages and right‑hand even pages.
            bool success = editor.MakeBooklet(
                inputPdf,
                outputPdf,
                leftHandOddPages.ToArray(),
                rightHandEvenPages.ToArray()
            );

            if (success)
                Console.WriteLine($"Booklet created successfully: {outputPdf}");
            else
                Console.Error.WriteLine("Booklet creation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during booklet creation: {ex.Message}");
        }
    }
}
