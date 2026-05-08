using System;
using System.IO;
using Aspose.Pdf;

class PortfolioFileRemover
{
    /// <summary>
    /// Removes an embedded file from a PDF portfolio by its zero‑based collection index.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF containing the portfolio.</param>
    /// <param name="fileIndex">Zero‑based index of the embedded file to delete.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    public static void RemoveEmbeddedFileByIndex(string inputPdf, int fileIndex, string outputPdf)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Access the collection of embedded files (portfolio).
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Validate the requested index.
            if (fileIndex < 0 || fileIndex >= embeddedFiles.Count)
            {
                Console.Error.WriteLine($"Invalid index {fileIndex}. Collection contains {embeddedFiles.Count} files.");
                return;
            }

            // The collection is keyed by the embedded file name.
            // Retrieve the name at the specified index and delete by name.
            string fileName = embeddedFiles.Keys[fileIndex];
            embeddedFiles.Delete(fileName);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Embedded file at index {fileIndex} removed. Saved to '{outputPdf}'.");
    }

    // Example usage.
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_updated.pdf";
        const int    removeIdx  = 2; // remove the third file (zero‑based)

        RemoveEmbeddedFileByIndex(inputPath, removeIdx, outputPath);
    }
}