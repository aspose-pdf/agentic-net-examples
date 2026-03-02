using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        const string outputPdf = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Load the first document as the base document
        using (Document mergedDoc = new Document(pdfFiles[0]))
        {
            // Append the remaining documents page by page
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document srcDoc = new Document(pdfFiles[i]))
                {
                    mergedDoc.Pages.Add(srcDoc.Pages);
                }
            }

            // Save the merged result as PDF
            mergedDoc.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}