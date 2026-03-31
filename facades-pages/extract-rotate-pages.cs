using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 2, 4, 5 };
        // Rotation to apply to each extracted page
        Rotation rotationToApply = Rotation.on90;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(inputPath))
        {
            // Create a new empty PDF for the result
            using (Document resultDoc = new Document())
            {
                foreach (int pageNumber in pagesToExtract)
                {
                    if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageNumber} is out of range.");
                        continue;
                    }

                    // Add a copy of the selected page to the result document
                    resultDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Rotate the newly added page (it is the last page in resultDoc)
                    int lastPageIndex = resultDoc.Pages.Count; // 1‑based index
                    resultDoc.Pages[lastPageIndex].Rotate = rotationToApply;
                }

                // Save the new PDF containing only the rotated pages
                resultDoc.Save(outputPath);
                Console.WriteLine($"Extracted and rotated pages saved to '{outputPath}'.");
            }
        }
    }
}