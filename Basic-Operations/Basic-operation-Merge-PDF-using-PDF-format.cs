using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Output merged PDF file
        string outputFile = "merged_output.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Error: Input file not found - {file}");
                return;
            }
        }

        try
        {
            // Load the first document (this will be the base document)
            Document mergedDoc = new Document(inputFiles[0]);

            // Merge each subsequent document into the base document
            for (int i = 1; i < inputFiles.Length; i++)
            {
                Document docToMerge = new Document(inputFiles[i]);
                // Use Aspose.Pdf's page collection merging (no MergeOptions needed)
                mergedDoc.Pages.Add(docToMerge.Pages);
            }

            // Save the merged document
            mergedDoc.Save(outputFile);
            Console.WriteLine($"Successfully merged PDFs into '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during merging: {ex.Message}");
        }
    }
}
