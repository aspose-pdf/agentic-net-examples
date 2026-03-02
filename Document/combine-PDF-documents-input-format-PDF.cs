using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters)
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        const string outputFile = "merged_output.pdf";

        // Validate input files
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Merge PDFs using Aspose.Pdf Document API
        // The first document becomes the target; subsequent documents are added page‑by‑page.
        using (Document target = new Document(inputFiles[0]))
        {
            for (int i = 1; i < inputFiles.Length; i++)
            {
                using (Document source = new Document(inputFiles[i]))
                {
                    // Append all pages from the source document to the target document
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged document
            target.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}