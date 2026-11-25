using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string leftPdfPath  = "left.pdf";
        const string rightPdfPath = "right.pdf";
        const string diffMdPath   = "diff.md";

        if (!File.Exists(leftPdfPath))
        {
            Console.Error.WriteLine($"File not found: {leftPdfPath}");
            return;
        }
        if (!File.Exists(rightPdfPath))
        {
            Console.Error.WriteLine($"File not found: {rightPdfPath}");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document leftDoc = new Document(leftPdfPath))
            using (Document rightDoc = new Document(rightPdfPath))
            {
                // ComparisonOptions can be customised if needed; default works for flat text diff
                var options = new ComparisonOptions();

                // Perform flat text comparison; result is a list of DiffOperation objects
                var diffOperations = TextPdfComparer.CompareFlatDocuments(leftDoc, rightDoc, options);

                // Convert the list of DiffOperation objects into a Markdown‑formatted string.
                // Each DiffOperation's ToString() provides a readable description; we join them with line breaks.
                string markdownDiff = string.Join(Environment.NewLine, diffOperations.Select(op => op.ToString()));

                // Write the diff to a .md file
                File.WriteAllText(diffMdPath, markdownDiff);
            }

            Console.WriteLine($"Markdown diff saved to '{diffMdPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
