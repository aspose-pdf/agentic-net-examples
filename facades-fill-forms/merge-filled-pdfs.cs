using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths of the filled PDF files generated from separate DataTables
        string[] inputFiles = new string[] { "filled1.pdf", "filled2.pdf", "filled3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all source files exist
        foreach (string filePath in inputFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                return;
            }
        }

        // Create an empty document and merge the source PDFs into it
        using (Document mergedDocument = new Document())
        {
            mergedDocument.Merge(inputFiles);
            mergedDocument.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}