using System;
using System.IO;
using Aspose.Pdf;

class PdfMerger
{
    static void Main()
    {
        // Input PDF files to be merged – adjust paths as needed
        string[] inputFiles = new string[]
        {
            "first.pdf",
            "second.pdf",
            "third.pdf"
        };

        // Output merged PDF file
        const string outputFile = "merged_output.pdf";

        // Validate that all input files exist
        foreach (string path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load the first document as the target; it will receive pages and bookmarks from the others
        using (Document target = new Document(inputFiles[0]))
        {
            // Merge each subsequent document into the target.
            // The Document.Merge method preserves outlines/bookmarks from the source document.
            for (int i = 1; i < inputFiles.Length; i++)
            {
                using (Document source = new Document(inputFiles[i]))
                {
                    target.Merge(source);
                }
            }

            // Save the combined document; bookmarks and outline hierarchy are retained
            target.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}
