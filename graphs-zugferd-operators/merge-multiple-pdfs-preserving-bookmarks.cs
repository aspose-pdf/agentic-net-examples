using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters)
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all source files exist
        foreach (var path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Source file not found: {path}");
                return;
            }
        }

        // Load the first document – it will become the target document
        using (Document mergedDoc = new Document(inputFiles[0]))
        {
            // Merge the remaining documents one by one.
            // The Document.Merge method copies pages and preserves
            // bookmarks/outlines hierarchy from the source document.
            for (int i = 1; i < inputFiles.Length; i++)
            {
                using (Document srcDoc = new Document(inputFiles[i]))
                {
                    mergedDoc.Merge(srcDoc);
                }
            }

            // Save the combined PDF with all bookmarks intact
            mergedDoc.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}