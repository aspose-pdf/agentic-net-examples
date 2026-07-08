using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains embedded (portfolio) items
        const string pdfPath = "portfolio.pdf";

        // Index of the embedded item to extract (1‑based as per Aspose.Pdf docs)
        const int itemIndex = 1;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the recommended disposal pattern for Document objects
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count < itemIndex)
            {
                Console.Error.WriteLine($"No embedded file at index {itemIndex}.");
                return;
            }

            // Retrieve the embedded file specification (indexer is 1‑based)
            FileSpecification embeddedFile = doc.EmbeddedFiles[itemIndex];

            // Original file name (includes the original extension). Fallback if Name is null/empty.
            string outputFileName = !string.IsNullOrEmpty(embeddedFile.Name)
                ? embeddedFile.Name
                : $"embedded_{itemIndex}";

            // Extract the file content via the Contents stream and write it to disk
            using (Stream source = embeddedFile.Contents)
            using (FileStream destination = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
            {
                source.CopyTo(destination);
            }

            Console.WriteLine($"Embedded item saved as \"{outputFileName}\".");
        }
    }
}
