using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by task specification

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the source PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(inputPath))
        {
            // Preserve the original XMP metadata (stored in the Metadata dictionary)
            var originalMetadata = sourceDoc.Metadata;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Create a new document for the single page
                using (Aspose.Pdf.Document singleDoc = new Aspose.Pdf.Document())
                {
                    // Add the current page from the source document
                    singleDoc.Pages.Add(sourceDoc.Pages[i]);

                    // Copy all metadata entries to the new document
                    foreach (var key in originalMetadata.Keys)
                    {
                        singleDoc.Metadata[key] = originalMetadata[key];
                    }

                    // Save the split PDF
                    string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                    singleDoc.Save(outPath);
                    Console.WriteLine($"Saved page {i} → {outPath}");
                }
            }
        }

        Console.WriteLine("Splitting complete with XMP metadata preserved.");
    }
}