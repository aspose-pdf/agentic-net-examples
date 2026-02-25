using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        // Output merged PDF
        const string outputPath = "merged.pdf";

        // Validate input files
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Use the first document as the target and merge the rest into it
        using (Document target = new Document(pdfFiles[0]))
        {
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document source = new Document(pdfFiles[i]))
                {
                    // Append all pages from the source document
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged document
            target.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}