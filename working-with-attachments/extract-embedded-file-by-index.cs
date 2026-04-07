using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains embedded files (portfolio items)
        const string pdfPath = "portfolio.pdf";

        // Index of the embedded file to extract (1‑based as per Aspose.Pdf docs)
        const int embeddedFileIndex = 1;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the document actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count < embeddedFileIndex)
            {
                Console.Error.WriteLine($"No embedded file found at index {embeddedFileIndex}.");
                return;
            }

            // Retrieve the embedded file specification (indexer is 1‑based)
            FileSpecification fileSpec = doc.EmbeddedFiles[embeddedFileIndex];

            // The original file name (including extension) is stored in the Name property
            // If for some reason it is missing, fall back to a generated name.
            string originalFileName = !string.IsNullOrEmpty(fileSpec.Name)
                ? fileSpec.Name
                : $"embedded_{embeddedFileIndex}";

            // Extract the raw bytes of the embedded file using the Contents stream
            byte[] fileBytes;
            using (Stream contentStream = fileSpec.Contents)
            using (MemoryStream ms = new MemoryStream())
            {
                contentStream.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            // Build the output path – saved with its original file name and extension
            string outputPath = Path.Combine(
                Path.GetDirectoryName(pdfPath) ?? string.Empty,
                originalFileName);

            // Write the extracted file to disk
            File.WriteAllBytes(outputPath, fileBytes);

            Console.WriteLine($"Embedded file extracted and saved to: {outputPath}");
        }
    }
}
