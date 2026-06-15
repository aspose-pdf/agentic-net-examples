using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains embedded (portfolio) files
        const string inputPdfPath = "input.pdf";

        // Index of the embedded file to extract (1‑based as per Aspose.Pdf API)
        const int portfolioIndex = 1;

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the collection of embedded files (portfolio items)
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Validate the requested index
            if (embeddedFiles == null || embeddedFiles.Count < portfolioIndex)
            {
                Console.Error.WriteLine($"No embedded file at index {portfolioIndex}.");
                return;
            }

            // Retrieve the FileSpecification for the requested embedded file (1‑based indexer)
            FileSpecification fileSpec = embeddedFiles[portfolioIndex];

            // The original file name (including its extension) is stored in FileSpecification.Name
            string originalFileName = fileSpec.Name;

            // Determine the output path – saved with its original name in an "output" folder
            string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
            Directory.CreateDirectory(outputDirectory);
            string outputPath = Path.Combine(outputDirectory, originalFileName);

            // Save the embedded file to disk using its original name and extension
            using (Stream contentStream = fileSpec.Contents)
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                contentStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Embedded file extracted: {outputPath}");
        }
    }
}
