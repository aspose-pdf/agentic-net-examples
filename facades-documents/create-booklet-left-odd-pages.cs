using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the input PDF stream for processing
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Determine the total number of pages using a temporary Document instance
            int pageCount;
            using (Document tempDoc = new Document(inputStream))
            {
                pageCount = tempDoc.Pages.Count; // 1‑based page indexing
            }

            // Reset the stream position so PdfFileEditor can read from the beginning
            inputStream.Position = 0;

            // Build left (odd) and right (even) page arrays
            List<int> leftPages  = new List<int>();
            List<int> rightPages = new List<int>();

            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 1) // odd page numbers -> left side
                    leftPages.Add(i);
                else           // even page numbers -> right side
                    rightPages.Add(i);
            }

            // Prepare the output stream
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Create the PdfFileEditor facade and generate the booklet with custom page ordering
                PdfFileEditor editor = new PdfFileEditor();
                bool result = editor.MakeBooklet(
                    inputStream,
                    outputStream,
                    leftPages.ToArray(),
                    rightPages.ToArray()
                );

                Console.WriteLine(result
                    ? $"Booklet created successfully: {outputPath}"
                    : "Failed to create booklet.");
            }
        }
    }
}