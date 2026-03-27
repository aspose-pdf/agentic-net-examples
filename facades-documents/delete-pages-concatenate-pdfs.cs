using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf" };
        // Pages to delete (1‑based indexing as required by PdfFileEditor)
        int[] pagesToDelete = new int[] { 2, 3 };
        // List to hold the names of the intermediate cleaned PDFs that were created successfully
        List<string> cleanedFiles = new List<string>();

        PdfFileEditor editor = new PdfFileEditor();

        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputPath = inputFiles[i];
            string cleanedPath = $"clean{i + 1}.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Delete the specified pages and write the result to a new file
            bool deleteResult = editor.Delete(inputPath, pagesToDelete, cleanedPath);
            if (!deleteResult || !File.Exists(cleanedPath))
            {
                Console.Error.WriteLine($"Failed to delete pages from {inputPath} or output file was not created.");
                continue;
            }

            cleanedFiles.Add(cleanedPath);
        }

        // Concatenate the cleaned PDFs into a single document (only if we have at least one file)
        string mergedOutput = "merged.pdf";
        if (cleanedFiles.Count == 0)
        {
            Console.Error.WriteLine("No cleaned PDFs were generated – merging aborted.");
            return;
        }

        // Use Aspose.Pdf.Document merging (more robust than PdfFileEditor.Concatenate)
        Document mergedDoc = new Document();
        foreach (string file in cleanedFiles)
        {
            Document part = new Document(file);
            // Append all pages of the part document to the merged document
            mergedDoc.Pages.Add(part.Pages);
        }
        mergedDoc.Save(mergedOutput);
        Console.WriteLine($"Merged PDF saved to '{mergedOutput}'.");
    }
}
