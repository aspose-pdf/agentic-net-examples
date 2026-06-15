using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath1 = "portfolio1.pdf";
        const string portfolioPath2 = "portfolio2.pdf";
        const string outputPath     = "merged_portfolio.pdf";

        // Verify input files exist
        if (!File.Exists(portfolioPath1) || !File.Exists(portfolioPath2))
        {
            Console.Error.WriteLine("One or both input portfolio files were not found.");
            return;
        }

        try
        {
            // Load both portfolios inside using blocks for deterministic disposal
            using (Document doc1 = new Document(portfolioPath1))
            using (Document doc2 = new Document(portfolioPath2))
            {
                // Merge pages of the second portfolio into the first
                doc1.Merge(doc2);

                // Preserve embedded files (portfolio items) from the second document
                foreach (FileSpecification ef in doc2.EmbeddedFiles)
                {
                    // Original file name stored in the FileSpecification
                    string originalName = ef.Name;
                    if (string.IsNullOrEmpty(originalName))
                        continue; // skip if name cannot be determined

                    // Ensure a unique name in the target document
                    string newName = originalName;
                    int suffix = 1;
                    while (doc1.EmbeddedFiles.Any(e => e.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                    {
                        newName = $"{Path.GetFileNameWithoutExtension(originalName)}_{suffix}{Path.GetExtension(originalName)}";
                        suffix++;
                    }

                    // The file data is available via the Contents stream of the FileSpecification
                    Stream fileData = ef.Contents;
                    if (fileData != null)
                    {
                        // Create a new FileSpecification for the target document and assign the stream
                        var fileSpec = new FileSpecification(newName, "Embedded from merged portfolio");
                        // Reset stream position in case it was read earlier
                        if (fileData.CanSeek)
                            fileData.Position = 0;
                        fileSpec.Contents = fileData;
                        doc1.EmbeddedFiles.Add(fileSpec);
                    }
                }

                // Preserve document‑level metadata (Info dictionary) from the second portfolio
                foreach (var entry in doc2.Info)
                {
                    // If the key does not exist in the first document, copy it
                    if (!doc1.Info.ContainsKey(entry.Key))
                    {
                        doc1.Info[entry.Key] = entry.Value;
                    }
                }

                // Save the combined portfolio
                doc1.Save(outputPath);
            }

            Console.WriteLine($"Merged portfolio saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}
