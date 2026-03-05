using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output merged PDF file
        const string outputFile = "merged.pdf";

        // Validate input files
        foreach (string path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        try
        {
            // Load the first PDF as the base document
            using (Document mergedDoc = new Document(inputFiles[0]))
            {
                // Merge remaining PDFs into the base document
                for (int i = 1; i < inputFiles.Length; i++)
                {
                    using (Document srcDoc = new Document(inputFiles[i]))
                    {
                        mergedDoc.Pages.Add(srcDoc.Pages);
                    }
                }

                // Save the merged document as PDF
                mergedDoc.Save(outputFile);
            }

            Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}