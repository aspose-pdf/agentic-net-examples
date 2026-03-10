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

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // Load the first document – it will become the target document
            using (Document target = new Document(inputFiles[0]))
            {
                // Load and merge each subsequent document
                for (int i = 1; i < inputFiles.Length; i++)
                {
                    using (Document source = new Document(inputFiles[i]))
                    {
                        // Append all pages from the source document to the target
                        target.Pages.Add(source.Pages);
                    }
                }

                // Save the merged PDF
                target.Save(outputFile);
            }

            Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}