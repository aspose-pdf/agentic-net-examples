using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        const string outputPath = "merged.pdf";

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an empty document, merge the PDFs, and save the result
        using (Aspose.Pdf.Document mergedDoc = new Aspose.Pdf.Document())
        {
            // Merge using the file paths directly
            mergedDoc.Merge(inputFiles);

            // Save the merged document
            mergedDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}