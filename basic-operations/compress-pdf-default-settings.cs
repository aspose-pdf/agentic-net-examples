using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Optimization; // For optional optimization options (if needed)

class Program
{
    static void Main()
    {
        // Paths to the original and the compressed PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Get the original file size (in bytes)
        long originalSize = new FileInfo(inputPath).Length;
        Console.WriteLine($"Original size: {originalSize:N0} bytes");

        // Load the PDF, then save it using the default compression settings
        // The Document is wrapped in a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Optional: invoke OptimizeResources with default options to further reduce size
            // pdfDocument.OptimizeResources(); // uncomment if additional resource optimization is desired

            // Save the document – without specifying SaveOptions, Aspose.Pdf uses its default
            // compression (object streams, Flate compression, etc.)
            pdfDocument.Save(outputPath);
        }

        // Get the compressed file size (in bytes)
        long compressedSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Compressed size: {compressedSize:N0} bytes");

        // Report the size reduction
        long reduction = originalSize - compressedSize;
        double percent = originalSize > 0 ? (double)reduction / originalSize * 100 : 0;
        Console.WriteLine($"Size reduced by: {reduction:N0} bytes ({percent:F2}%)");
    }
}