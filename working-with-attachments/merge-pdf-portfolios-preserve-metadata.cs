using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolio1 = "portfolio1.pdf";
        const string portfolio2 = "portfolio2.pdf";
        const string outputPath = "merged_portfolio.pdf";

        // Verify input files exist
        if (!File.Exists(portfolio1) || !File.Exists(portfolio2))
        {
            Console.Error.WriteLine("One or both input portfolio files are missing.");
            return;
        }

        // ==== MERGE OPERATION ====
        using (Document target = new Document(portfolio1))
        using (Document source = new Document(portfolio2))
        {
            // 1. Merge pages
            target.Pages.Add(source.Pages);

            // 2. Merge embedded files (portfolio items) preserving their metadata
            foreach (FileSpecification srcSpec in source.EmbeddedFiles)
            {
                // Ensure unique name in the target collection
                string newName = srcSpec.Name;
                int duplicateIndex = 1;
                while (target.EmbeddedFiles.Any(f => f.Name == newName))
                {
                    newName = $"{srcSpec.Name}_{duplicateIndex}";
                    duplicateIndex++;
                }

                // Copy the file data into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Stream srcStream = srcSpec.Contents)
                    {
                        srcStream.CopyTo(ms);
                    }
                    ms.Position = 0;

                    // Create a new FileSpecification for the target document
                    // Constructor order: (Stream fileData, string name, string description)
                    FileSpecification tgtSpec = new FileSpecification(ms, newName, srcSpec.Description);

                    // Add to the target's embedded files collection
                    target.EmbeddedFiles.Add(tgtSpec);
                }
            }

            // 3. Save the merged portfolio
            target.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF portfolio saved to '{outputPath}'.");
    }
}
