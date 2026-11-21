using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the split PDF files
        const string inputDir = "SplitPages";
        // Path for the final merged PDF
        const string outputPath = "merged_selected.pdf";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Get all PDF files in the directory, sorted to ensure deterministic order
        var pdfFiles = Directory.GetFiles(inputDir, "*.pdf")
                                .OrderBy(f => f)
                                .ToArray();

        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to merge.");
            return;
        }

        // Load the first PDF as the base document
        Document mergedDoc = new Document(pdfFiles[0]);

        // Append pages from each subsequent PDF
        for (int i = 1; i < pdfFiles.Length; i++)
        {
            string srcPath = pdfFiles[i];
            using (Document srcDoc = new Document(srcPath))
            {
                // Add each page from the source document to the merged document
                foreach (Page page in srcDoc.Pages)
                {
                    mergedDoc.Pages.Add(page);
                }
            }
        }

        // Save the merged PDF
        mergedDoc.Save(outputPath);
        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
