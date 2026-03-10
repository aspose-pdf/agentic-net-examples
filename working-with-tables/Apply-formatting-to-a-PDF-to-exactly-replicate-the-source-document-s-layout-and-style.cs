using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, etc.)

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF that will replicate the layout.
        const string sourcePath = "source.pdf";
        const string outputPath = "replicated.pdf";

        // Verify that the source file exists before proceeding.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Error: File not found – {sourcePath}");
            return;
        }

        // Load the source PDF. The Document constructor handles loading.
        // The using statement ensures deterministic disposal of resources.
        using (Document pdfDoc = new Document(sourcePath))
        {
            // No modifications are required; saving the document as‑is preserves
            // the original layout, fonts, colors, images, annotations, etc.
            // Save(string) without explicit SaveOptions writes a PDF regardless of
            // the file extension, which is appropriate here.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF layout replicated successfully to '{outputPath}'.");
    }
}