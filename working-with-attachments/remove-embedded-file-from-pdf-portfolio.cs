using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_updated.pdf";
        // Index of the embedded file to delete (1‑based for user convenience)
        int fileIndex = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of embedded files in the portfolio
            var embeddedFiles = doc.EmbeddedFiles;

            // Validate the requested index (user supplied is 1‑based)
            if (fileIndex < 1 || fileIndex > embeddedFiles.Count)
            {
                Console.Error.WriteLine($"Invalid index {fileIndex}. Collection contains {embeddedFiles.Count} items.");
                return;
            }

            // Convert to zero‑based index for the collection
            int zeroBasedIndex = fileIndex - 1;

            // Get the name of the file at the specified index
            string fileName = embeddedFiles[zeroBasedIndex].Name;

            // Delete the embedded file by its name
            embeddedFiles.Delete(fileName);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Embedded file at index {fileIndex} removed. Saved to '{outputPath}'.");
    }
}
