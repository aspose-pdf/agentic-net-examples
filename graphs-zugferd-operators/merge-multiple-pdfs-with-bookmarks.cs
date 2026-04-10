using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters for outline hierarchy)
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all input files exist before proceeding
        foreach (var path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load each source document; keep them alive until the merge is complete
        Document[] sourceDocs = new Document[inputFiles.Length];
        try
        {
            for (int i = 0; i < inputFiles.Length; i++)
            {
                sourceDocs[i] = new Document(inputFiles[i]); // load PDF
            }

            // Create an empty target document that will receive the merged content
            using (Document target = new Document())
            {
                // Merge all source documents into the target.
                // This method preserves bookmarks and outline hierarchy by default.
                target.Merge(sourceDocs);

                // Save the merged PDF to the specified output path
                target.Save(outputFile);
            }

            Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
        }
        finally
        {
            // Ensure all source documents are properly disposed
            foreach (var doc in sourceDocs)
            {
                doc?.Dispose();
            }
        }
    }
}