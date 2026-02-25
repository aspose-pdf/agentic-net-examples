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

        // Verify that all input files exist
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // The first document will serve as the target; the rest are sources
        using (Document target = new Document(pdfFiles[0]))
        {
            // Iterate over remaining PDFs and merge them into the target
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document source = new Document(pdfFiles[i]))
                {
                    // Append all pages from the source document
                    target.Pages.Add(source.Pages);
                } // source disposed here
            }

            // Save the combined document
            target.Save(outputPath);
        } // target disposed here

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}