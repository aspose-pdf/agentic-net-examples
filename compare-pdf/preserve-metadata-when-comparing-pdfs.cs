using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the PDF to compare against, and the diff result.
        const string sourcePdfPath = "source.pdf";
        const string comparePdfPath = "compare.pdf";
        const string diffPdfPath = "diff.pdf";

        // -----------------------------------------------------------------
        // 1. Ensure source and compare PDFs exist – create simple placeholders
        //    if they are missing. This prevents the FileNotFoundException that
        //    occurs when the sample is run without external files.
        // -----------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("This is the SOURCE PDF."));
                // Set some example metadata on the source document.
                doc.Info.Author = "John Doe";
                doc.Info.Title = "Source Document";
                doc.Info.Subject = "Demo";
                doc.Info.Keywords = "Aspose, PDF, Sample";
                doc.Info.Creator = "Aspose.PDF Sample Generator";
                doc.Info.Producer = "Aspose.PDF";
                doc.Info.CreationDate = DateTime.Now;
                doc.Info.ModDate = DateTime.Now;
                // Example of a custom property.
                doc.Info["CustomProp"] = "CustomValue";
                doc.Save(sourcePdfPath);
            }
        }

        if (!File.Exists(comparePdfPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("This is the COMPARE PDF (slightly different)."));
                doc.Save(comparePdfPath);
            }
        }

        // -----------------------------------------------------------------
        // 2. Load the two PDFs to be compared.
        // -----------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (Document compareDoc = new Document(comparePdfPath))
        {
            // -----------------------------------------------------------------
            // 3. Perform a page‑by‑page comparison.
            //    The overload that accepts a result path creates the diff PDF.
            // -----------------------------------------------------------------
            ComparisonOptions options = new ComparisonOptions();
            // (options can be customized here if needed)

            TextPdfComparer.CompareDocumentsPageByPage(
                sourceDoc,
                compareDoc,
                options,
                diffPdfPath);
        }

        // -----------------------------------------------------------------
        // 4. Load the generated diff PDF and copy metadata from the source.
        // -----------------------------------------------------------------
        using (Document diffDoc = new Document(diffPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            DocumentInfo srcInfo = sourceDoc.Info;
            DocumentInfo dstInfo = diffDoc.Info;

            // Copy predefined metadata fields.
            dstInfo.Author       = srcInfo.Author;
            dstInfo.Title        = srcInfo.Title;
            dstInfo.Subject      = srcInfo.Subject;
            dstInfo.Keywords     = srcInfo.Keywords;
            dstInfo.Creator      = srcInfo.Creator;
            dstInfo.Producer     = srcInfo.Producer;
            dstInfo.CreationDate = srcInfo.CreationDate;
            dstInfo.ModDate      = srcInfo.ModDate;
            dstInfo.Trapped      = srcInfo.Trapped;

            // Copy custom metadata (custom properties) using the indexer.
            foreach (string key in srcInfo.Keys)
            {
                // Skip standard keys that were already copied to avoid duplication.
                if (IsStandardKey(key)) continue;
                dstInfo[key] = srcInfo[key];
            }

            // Overwrite the diff PDF with the updated metadata.
            diffDoc.Save(diffPdfPath);
        }

        Console.WriteLine($"Comparison complete. Diff PDF saved to '{diffPdfPath}' with original metadata preserved.");
    }

    // Helper to identify Aspose.Pdf predefined keys – this mirrors the fields
    // that are exposed as strongly‑typed properties on DocumentInfo.
    private static bool IsStandardKey(string key)
    {
        return string.Equals(key, "Author", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Title", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Subject", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Keywords", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Creator", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Producer", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "CreationDate", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "ModDate", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(key, "Trapped", StringComparison.OrdinalIgnoreCase);
    }
}
