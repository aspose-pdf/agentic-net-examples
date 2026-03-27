using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths of the split PDF parts that need to be merged.
        string[] splitFiles = new string[] { "part1.pdf", "part2.pdf", "part3.pdf" };

        // Verify that at least one file is supplied.
        if (splitFiles.Length == 0)
        {
            Console.WriteLine("No split files specified.");
            return;
        }

        // Find the first existing file – this will be the base document.
        int firstIndex = Array.FindIndex(splitFiles, File.Exists);
        if (firstIndex == -1)
        {
            Console.WriteLine("None of the specified split files were found on disk.");
            return;
        }

        // Load the first existing split PDF as the target document.
        using (Document targetDoc = new Document(splitFiles[firstIndex]))
        {
            // Insert the remaining PDFs (if they exist) at the end of the target document.
            for (int i = 0; i < splitFiles.Length; i++)
            {
                if (i == firstIndex) continue; // skip the file already loaded

                if (!File.Exists(splitFiles[i]))
                {
                    Console.WriteLine($"Warning: '{splitFiles[i]}' not found – skipping.");
                    continue;
                }

                using (Document sourceDoc = new Document(splitFiles[i]))
                {
                    // Insert pages after the current last page.
                    // Insert(pageNumber, sourcePages) expects a 1‑based page number.
                    targetDoc.Pages.Insert(targetDoc.Pages.Count + 1, sourceDoc.Pages);
                }
            }

            // Save the concatenated result.
            string outputPath = "merged.pdf";
            targetDoc.Save(outputPath);
            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
    }
}
