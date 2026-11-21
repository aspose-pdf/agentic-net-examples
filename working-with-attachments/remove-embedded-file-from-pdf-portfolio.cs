using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";          // PDF Portfolio to modify
        const string outputPath = "portfolio_updated.pdf";
        const int fileIndexToRemove = 2;                    // zero‑based index of the embedded file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF Portfolio inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The embedded files of a PDF (including a portfolio) are accessed via Document.EmbeddedFiles
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;
            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any embedded files.");
                return;
            }

            // Convert the zero‑based index supplied by the user to the 1‑based index used by the API.
            int apiIndex = fileIndexToRemove + 1;

            // Validate the index before removal
            if (apiIndex < 1 || apiIndex > embeddedFiles.Count)
            {
                Console.Error.WriteLine($"Invalid index {fileIndexToRemove}. PDF contains {embeddedFiles.Count} embedded files.");
                return;
            }

            // Retrieve the FileSpecification at the calculated index and delete it by name.
            FileSpecification fileSpec = embeddedFiles[apiIndex];
            string nameToDelete = fileSpec.Name;
            if (string.IsNullOrEmpty(nameToDelete))
            {
                Console.Error.WriteLine("Unable to determine the name of the file to delete.");
                return;
            }

            // Delete the file from the PDF portfolio.
            embeddedFiles.Delete(nameToDelete);

            // Save the modified PDF (still a portfolio)
            doc.Save(outputPath);
        }

        Console.WriteLine($"File at index {fileIndexToRemove} removed. Saved to '{outputPath}'.");
    }
}
