using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge (must exist on disk)
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        const string outputPath = "merged.pdf";

        // Validate input files
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Use the first document as the target; all others are sources
        using (Document target = new Document(pdfFiles[0]))
        {
            // Merge each subsequent PDF into the target document
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document source = new Document(pdfFiles[i]))
                {
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged result
            target.Save(outputPath);
        }

        Console.WriteLine($"PDF files merged successfully into '{outputPath}'.");
    }
}