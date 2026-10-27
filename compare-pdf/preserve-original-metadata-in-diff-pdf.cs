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

        // Load source and target documents
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Perform side‑by‑side visual comparison using the static comparer
            var options = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(sourceDoc, targetDoc, diffPath, options);
        }

        // Copy metadata from source to the generated diff PDF
        using (Document sourceDoc = new Document(sourcePath))
        using (Document diffDoc = new Document(diffPath))
        {
            // Standard metadata fields
            diffDoc.Info.Title        = sourceDoc.Info.Title;
            diffDoc.Info.Author       = sourceDoc.Info.Author;
            diffDoc.Info.Subject      = sourceDoc.Info.Subject;
            diffDoc.Info.Keywords     = sourceDoc.Info.Keywords;
            diffDoc.Info.Creator      = sourceDoc.Info.Creator;
            diffDoc.Info.Producer     = sourceDoc.Info.Producer;
            diffDoc.Info.CreationDate = sourceDoc.Info.CreationDate;
            diffDoc.Info.ModDate      = sourceDoc.Info.ModDate;

            // Custom metadata – use the indexer provided by DocumentInfo
            foreach (var name in sourceDoc.Info.Keys)
            {
                diffDoc.Info[name] = sourceDoc.Info[name];
            }

            diffDoc.Save(diffPath);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPath}' with original metadata preserved.");
    }
}
