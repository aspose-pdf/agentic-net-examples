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

        // Verify that all input files exist
        foreach (var path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load the first PDF as the target document
        using (Document target = new Document(inputFiles[0]))
        {
            // Merge each subsequent PDF into the target
            for (int i = 1; i < inputFiles.Length; i++)
            {
                using (Document source = new Document(inputFiles[i]))
                {
                    // Merge preserves bookmarks/outlines by default
                    target.Merge(source);
                }
            }

            // Save the merged document; bookmarks and outline hierarchy are retained
            target.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}