using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters for bookmark hierarchy)
        string[] inputFiles = { "chapter1.pdf", "chapter2.pdf", "appendix.pdf" };
        const string outputPath = "merged_document.pdf";

        // Verify that all source files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Load the first PDF as the base document (preserves its bookmarks)
        using (Document mergedDoc = new Document(inputFiles[0]))
        {
            // Merge the remaining PDFs; bookmarks/outlines are retained automatically
            string[] remainingFiles = inputFiles.Skip(1).ToArray();
            if (remainingFiles.Length > 0)
            {
                mergedDoc.Merge(remainingFiles);
            }

            // Save the combined PDF (bookmarks hierarchy is preserved)
            mergedDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}