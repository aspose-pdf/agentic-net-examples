using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the split PDF files (e.g., Page_1.pdf, Page_2.pdf, ...)
        const string splitFolder   = "SplitPages";
        // Path for the final concatenated PDF
        const string outputPdfPath = "combined.pdf";

        if (!Directory.Exists(splitFolder))
        {
            Console.Error.WriteLine($"Folder not found: {splitFolder}");
            return;
        }

        // Get all PDF files in the folder – adjust the search pattern if needed
        string[] splitFiles = Directory.GetFiles(splitFolder, "*.pdf");
        if (splitFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Create an empty target document
        using (Document target = new Document())
        {
            // Iterate over each split PDF and insert its pages at the end of the target
            foreach (string filePath in splitFiles)
            {
                using (Document source = new Document(filePath))
                {
                    // Pages are 1‑based in Aspose.Pdf
                    for (int i = 1; i <= source.Pages.Count; i++)
                    {
                        // Insert the page after the current last page (i.e., at position Count + 1)
                        target.Pages.Insert(target.Pages.Count + 1, source.Pages[i]);
                    }
                }
            }

            // Save the concatenated document
            target.Save(outputPdfPath);
        }

        Console.WriteLine($"Concatenated PDF saved to '{outputPdfPath}'.");
    }
}