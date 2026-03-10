using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        const string outputPath = "merged.pdf";

        // Verify that all input files exist before proceeding
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an empty Document, merge the PDFs, and save the result
        using (Document mergedDoc = new Document())
        {
            // Merge accepts an array of file paths and loads them internally
            mergedDoc.Merge(pdfFiles);

            // Save the merged document as PDF
            mergedDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}