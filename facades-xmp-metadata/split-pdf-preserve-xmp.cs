using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document sourceDoc = new Document(inputPath))
        {
            // Preserve the original XMP metadata (copy each entry via the indexer)
            var originalMetadata = sourceDoc.Metadata;

            int pageCount = sourceDoc.Pages.Count;
            // Evaluation mode allows a maximum of 4 pages per collection
            int maxPages = Math.Min(pageCount, 4);

            for (int i = 1; i <= maxPages; i++)
            {
                using (Document singleDoc = new Document())
                {
                    // Add the i‑th page from the source document (a copy is created internally)
                    singleDoc.Pages.Add(sourceDoc.Pages[i]);

                    // Copy all XMP metadata entries from the source document to the new document
                    foreach (string key in originalMetadata.Keys)
                    {
                        singleDoc.Metadata[key] = originalMetadata[key];
                    }

                    // If any custom namespaces were registered in the source, re‑register them
                    // (Aspose.Pdf does not expose a direct collection, so we re‑register common ones manually if needed)
                    // Example (uncomment and adjust as required):
                    // singleDoc.Metadata.RegisterNamespaceUri("my", "http://example.com/custom");

                    string outputFileName = $"page{i}.pdf";
                    singleDoc.Save(outputFileName);
                    Console.WriteLine("Saved " + outputFileName);
                }
            }

            if (pageCount > 4)
            {
                Console.WriteLine("Note: Evaluation mode limits processing to 4 pages. More pages would be handled similarly.");
            }
        }
    }
}
