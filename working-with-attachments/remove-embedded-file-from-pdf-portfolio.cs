using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_updated.pdf";
        const int fileIndex = 2; // 1‑based index of the embedded file to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF portfolio
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any embedded files.");
                return;
            }

            // Validate the index against the collection count (EmbeddedFileCollection is 1‑based)
            if (fileIndex < 1 || fileIndex > doc.EmbeddedFiles.Count)
            {
                Console.Error.WriteLine($"Invalid index: {fileIndex}. Portfolio contains {doc.EmbeddedFiles.Count} files.");
                return;
            }

            // Retrieve the file specification at the given 1‑based index
            FileSpecification fileSpec = doc.EmbeddedFiles[fileIndex];
            string attachmentName = fileSpec?.Name;

            if (string.IsNullOrEmpty(attachmentName))
            {
                Console.Error.WriteLine("Unable to determine the name of the embedded file to delete.");
                return;
            }

            // Remove the embedded file by its name
            doc.EmbeddedFiles.Delete(attachmentName);

            // Save the modified PDF portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Embedded file at index {fileIndex} removed. Saved to '{outputPath}'.");
    }
}
