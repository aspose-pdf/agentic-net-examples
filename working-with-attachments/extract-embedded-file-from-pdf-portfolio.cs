using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing portfolio (embedded) files
        const string inputPdfPath = "portfolio.pdf";

        // Zero‑based index of the portfolio item to extract (e.g., 0 for the first item)
        int itemIndexZeroBased = 0;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Aspose.Pdf collections are 1‑based; convert the zero‑based index
                int itemIndex = itemIndexZeroBased + 1;

                // Verify that the requested index exists
                if (itemIndex < 1 || itemIndex > doc.EmbeddedFiles.Count)
                {
                    Console.Error.WriteLine($"Invalid index. PDF contains {doc.EmbeddedFiles.Count} portfolio items.");
                    return;
                }

                // Retrieve the embedded file specification (Aspose.Pdf uses FileSpecification)
                FileSpecification fileSpec = doc.EmbeddedFiles[itemIndex];

                // The original file name (including extension) is stored in the Name property
                string originalFileName = fileSpec.Name;

                // Determine an output path – here we save to the current directory
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), originalFileName);

                // Write the raw bytes to disk preserving the original extension
                using (FileStream outStream = File.Create(outputPath))
                {
                    // fileSpec.Contents returns a Stream containing the embedded data
                    fileSpec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Extracted portfolio item saved as: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
