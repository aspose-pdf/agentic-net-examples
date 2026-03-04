using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "first.pdf", "second.pdf", "third.pdf" };
        const string outputFile = "merged.pdf";

        // Verify that all input files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create an empty document, merge the input PDFs, and save the result
        using (Document mergedDocument = new Document())
        {
            // Merge overload that accepts an array of file paths
            mergedDocument.Merge(inputFiles);

            // Save the merged document as PDF
            mergedDocument.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}