using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string modifiedPath = "modified.pdf";
        const string diffPath     = "diff.pdf";

        // Verify input files exist
        if (!File.Exists(originalPath) || !File.Exists(modifiedPath))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the two PDFs and perform a page‑by‑page comparison.
        //    The result is saved directly to diffPath.
        // ------------------------------------------------------------
        using (Document originalDoc = new Document(originalPath))
        using (Document modifiedDoc = new Document(modifiedPath))
        {
            ComparisonOptions options = new ComparisonOptions(); // default options
            TextPdfComparer.CompareDocumentsPageByPage(originalDoc, modifiedDoc, options, diffPath);
        }

        // ------------------------------------------------------------
        // 2. Load the generated diff PDF and copy all metadata from the
        //    original document. DocumentInfo implements IDictionary<string,string>,
        //    so we can enumerate its entries and assign them to the diff.
        // ------------------------------------------------------------
        using (Document originalDoc = new Document(originalPath))
        using (Document diffDoc = new Document(diffPath))
        {
            // Optional: clear any existing metadata in the diff PDF
            diffDoc.Info.Clear();

            // Copy every metadata entry (Title, Author, CreationDate, etc.)
            foreach (var kvp in originalDoc.Info)
            {
                diffDoc.Info[kvp.Key] = kvp.Value;
            }

            // Overwrite the diff PDF with the updated metadata
            diffDoc.Save(diffPath);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPath}' with original metadata preserved.");
    }
}