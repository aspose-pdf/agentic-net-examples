using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "first.pdf", "second.pdf" };
        // Output merged PDF file
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

        // Create an empty document, merge the input PDFs, and save the result.
        // The Document is wrapped in a using block to ensure proper disposal.
        using (Document mergedDoc = new Document())
        {
            // Merge the specified PDF files into the empty document.
            mergedDoc.Merge(inputFiles);

            // Save the merged document to the desired output path.
            mergedDoc.Save(outputFile);
        }

        Console.WriteLine($"PDF files merged successfully into '{outputFile}'.");
    }
}