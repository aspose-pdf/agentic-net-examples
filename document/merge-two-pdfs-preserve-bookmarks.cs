using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files – ensure they exist before proceeding
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged.pdf";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Wrap each Document in a using block for deterministic disposal
        using (Document target = new Document(firstPdfPath))
        using (Document source = new Document(secondPdfPath))
        {
            // Optional: configure merge options (e.g., rebalance page tree)
            Document.MergeOptions mergeOptions = new Document.MergeOptions {
                IsNeedPageTreeBalance = true
            };

            // Merge the source document into the target.
            // This operation preserves bookmarks (outlines) from both PDFs.
            target.Merge(mergeOptions, source);

            // Save the merged document.
            target.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}