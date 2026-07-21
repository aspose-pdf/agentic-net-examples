using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string diffPath   = "diff.pdf";

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Load the two PDFs to be compared
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Perform side‑by‑side comparison and write the result to diffPath
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(sourceDoc, targetDoc, diffPath, compareOptions);
        }

        // Load the generated diff PDF and copy metadata from the source PDF
        using (Document sourceDoc = new Document(sourcePath))
        using (Document diffDoc = new Document(diffPath))
        {
            // Copy all predefined and custom metadata entries
            foreach (var entry in sourceDoc.Info)
            {
                diffDoc.Info[entry.Key] = entry.Value;
            }

            // Save the diff PDF with the preserved metadata
            diffDoc.Save(diffPath);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPath}' with original metadata.");
    }
}