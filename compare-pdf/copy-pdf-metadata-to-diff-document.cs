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

        // ---------------------------------------------------------------------
        // Ensure the sample PDFs exist – create minimal documents with metadata
        // ---------------------------------------------------------------------
        CreateSamplePdfIfMissing(sourcePath, "Source Document", "John Doe", "Sample source PDF");
        CreateSamplePdfIfMissing(targetPath, "Target Document", "Jane Smith", "Sample target PDF");

        // Load the original PDFs and perform side‑by‑side comparison
        using (Document source = new Document(sourcePath))
        using (Document target = new Document(targetPath))
        {
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(source, target, diffPath, compareOptions);
        }

        // Load the generated diff PDF and copy metadata from the source PDF
        using (Document source = new Document(sourcePath))
        using (Document diff = new Document(diffPath))
        {
            // Copy standard metadata fields
            diff.Info.Title        = source.Info.Title;
            diff.Info.Author       = source.Info.Author;
            diff.Info.Subject      = source.Info.Subject;
            diff.Info.Keywords     = source.Info.Keywords;
            diff.Info.Creator      = source.Info.Creator;
            diff.Info.Producer     = source.Info.Producer;
            diff.Info.CreationDate = source.Info.CreationDate;
            diff.Info.ModDate      = source.Info.ModDate;
            diff.Info.Trapped      = source.Info.Trapped;

            // Copy any custom metadata entries (keys that are not predefined)
            foreach (var kvp in source.Info)
            {
                if (!DocumentInfo.IsPredefinedKey(kvp.Key))
                {
                    diff.Info[kvp.Key] = kvp.Value;
                }
            }

            // Overwrite the diff PDF with the updated metadata
            diff.Save(diffPath);
        }

        Console.WriteLine($"Comparison completed. Metadata from '{sourcePath}' copied to '{diffPath}'.");
    }

    /// <summary>
    /// Creates a very small PDF with basic metadata if the file does not already exist.
    /// </summary>
    private static void CreateSamplePdfIfMissing(string path, string title, string author, string subject)
    {
        if (File.Exists(path))
            return;

        using (Document doc = new Document())
        {
            // Add a single blank page so the document is not empty
            doc.Pages.Add();

            // Populate standard metadata fields
            doc.Info.Title   = title;
            doc.Info.Author  = author;
            doc.Info.Subject = subject;
            doc.Info.Creator = "Aspose.Pdf Sample Generator";
            doc.Info.Producer = "Aspose.Pdf";
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate = DateTime.Now;

            // Example of a custom metadata entry
            doc.Info["CustomKey"] = "CustomValue";

            doc.Save(path);
        }
    }
}
