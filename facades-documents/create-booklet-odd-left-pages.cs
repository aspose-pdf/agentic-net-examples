using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the input PDF as a stream.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Determine the total number of pages using Document (lifecycle rule: use Document for load/save).
            int pageCount;
            using (Document tempDoc = new Document(inputStream))
            {
                pageCount = tempDoc.Pages.Count;
            }

            // Reset the stream position after reading the document.
            inputStream.Seek(0, SeekOrigin.Begin);

            // Build left (odd) and right (even) page arrays.
            // Aspose.Pdf uses 1‑based page indexing.
            int oddCount  = (pageCount + 1) / 2; // number of odd pages
            int evenCount = pageCount / 2;       // number of even pages

            int[] leftPages  = new int[oddCount];
            int[] rightPages = new int[evenCount];

            int oddIdx = 0, evenIdx = 0;
            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 1) // odd page -> left side
                    leftPages[oddIdx++] = i;
                else           // even page -> right side
                    rightPages[evenIdx++] = i;
            }

            // Prepare the output stream.
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Use PdfFileEditor to create a customized booklet.
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);

                if (success)
                    Console.WriteLine($"Booklet created successfully: {outputPath}");
                else
                    Console.Error.WriteLine("Failed to create booklet.");
            }
        }
    }
}