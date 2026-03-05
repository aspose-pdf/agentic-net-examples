using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an optimization strategy with all available options enabled
            OptimizationOptions opt = OptimizationOptions.All();

            // Fine‑tune specific options (optional, but demonstrates typical settings)
            opt.CompressObjects = true;          // Pack PDF objects into streams and compress them
            opt.SubsetFonts = true;              // Embed only the glyphs actually used
            opt.RemoveUnusedObjects = true;      // Delete objects that are not referenced
            opt.RemoveUnusedStreams = true;      // Remove resource streams that are never used
            opt.LinkDuplicateStreams = true;     // Merge identical streams to a single object

            // Apply the resource optimization to the document
            doc.OptimizeResources(opt);

            // Linearize the PDF for faster web access (optional but often useful)
            doc.Optimize();

            // Save the compressed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}