using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Load both PDFs
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Copy XMP metadata (stored in the Metadata dictionary) from source to target
            foreach (var entry in sourceDoc.Metadata)
            {
                // Overwrite or add the metadata entry in the target document
                targetDoc.Metadata[entry.Key] = entry.Value;
            }

            // Merge all pages from the source into the target
            targetDoc.Pages.Add(sourceDoc.Pages);

            // Save the merged PDF
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
