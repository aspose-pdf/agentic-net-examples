using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "first.pdf", "second.pdf", "third.pdf" };
        const string outputPath = "merged.pdf";

        // Verify that all input files exist before proceeding
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an empty document, merge the PDFs, and save the result
        using (Document mergedDoc = new Document())
        {
            // Merge the specified PDF files into the empty document
            mergedDoc.Merge(inputFiles);

            // Save the merged document as a PDF
            mergedDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}