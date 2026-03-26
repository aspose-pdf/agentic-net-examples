using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string modifiedPath = "modified.pdf";
        const string diffPath = "diff.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(modifiedPath))
        {
            Console.Error.WriteLine($"Modified file not found: {modifiedPath}");
            return;
        }

        // Load the two PDFs to be compared
        using (Document sourceDoc = new Document(sourcePath))
        using (Document modifiedDoc = new Document(modifiedPath))
        {
            // Generate side‑by‑side diff PDF
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(sourceDoc, modifiedDoc, diffPath, options);
        }

        // Load the diff PDF and copy metadata from the original source PDF
        using (Document sourceDoc = new Document(sourcePath))
        using (Document diffDoc = new Document(diffPath))
        {
            diffDoc.Info.Title = sourceDoc.Info.Title;
            diffDoc.Info.Author = sourceDoc.Info.Author;
            diffDoc.Info.Subject = sourceDoc.Info.Subject;
            diffDoc.Info.Keywords = sourceDoc.Info.Keywords;
            diffDoc.Info.Creator = sourceDoc.Info.Creator;
            diffDoc.Info.Producer = sourceDoc.Info.Producer;
            diffDoc.Info.CreationDate = sourceDoc.Info.CreationDate;
            diffDoc.Info.ModDate = sourceDoc.Info.ModDate;
            diffDoc.Info.Trapped = sourceDoc.Info.Trapped;

            // Save the diff PDF with the transferred metadata
            diffDoc.Save(diffPath);
        }

        Console.WriteLine($"Comparison complete. Diff PDF saved to '{diffPath}' with original metadata.");
    }
}