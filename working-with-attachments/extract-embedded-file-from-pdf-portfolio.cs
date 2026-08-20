using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains a portfolio (embedded files)
        const string pdfPath = "portfolio.pdf";

        // Index of the embedded file to extract (1‑based as per Aspose.Pdf docs)
        const int embeddedIndex = 2;

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Access the collection of embedded files (portfolio items)
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Validate the requested index
            if (embeddedIndex < 1 || embeddedIndex > embeddedFiles.Count)
            {
                Console.Error.WriteLine($"Invalid index {embeddedIndex}. " +
                                        $"Document contains {embeddedFiles.Count} embedded file(s).");
                return;
            }

            // Retrieve the specific embedded file (FileSpecification)
            FileSpecification fileSpec = embeddedFiles[embeddedIndex];

            // Determine the original file name (includes its extension)
            // Use the Name property – the correct way to get the embedded file's name
            string originalFileName = fileSpec.Name ?? $"embedded_{embeddedIndex}";

            // Save the embedded file using its original name
            using (Stream source = fileSpec.Contents)
            using (FileStream destination = new FileStream(originalFileName, FileMode.Create, FileAccess.Write))
            {
                source.CopyTo(destination);
            }

            Console.WriteLine($"Embedded file extracted and saved as: {originalFileName}");
        }
    }
}
