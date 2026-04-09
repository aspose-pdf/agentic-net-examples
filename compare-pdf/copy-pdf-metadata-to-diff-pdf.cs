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

        // Verify input files exist
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load source and target documents.
        // -----------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // -----------------------------------------------------------------
            // 2. Perform side‑by‑side comparison. Use the overload that writes the
            //    result directly to a file (string) – this matches the expected
            //    parameter types of the Aspose.Pdf.Comparison API.
            // -----------------------------------------------------------------
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(sourceDoc, targetDoc, diffPath, sideOptions);

            // -----------------------------------------------------------------
            // 3. Re‑open the generated diff PDF so we can copy metadata from the
            //    source document.
            // -----------------------------------------------------------------
            using (Document diffDoc = new Document(diffPath))
            {
                // -----------------------------------------------------------------
                // 4. Clear any existing metadata in the diff document (optional).
                // -----------------------------------------------------------------
                diffDoc.Info.Clear();

                // -----------------------------------------------------------------
                // 5. Copy predefined metadata fields from source to diff.
                // -----------------------------------------------------------------
                diffDoc.Info.Title        = sourceDoc.Info.Title;
                diffDoc.Info.Author       = sourceDoc.Info.Author;
                diffDoc.Info.Subject      = sourceDoc.Info.Subject;
                diffDoc.Info.Keywords     = sourceDoc.Info.Keywords;
                diffDoc.Info.Creator      = sourceDoc.Info.Creator;
                diffDoc.Info.Producer     = sourceDoc.Info.Producer;
                diffDoc.Info.CreationDate = sourceDoc.Info.CreationDate;
                diffDoc.Info.ModDate      = sourceDoc.Info.ModDate;
                diffDoc.Info.Trapped      = sourceDoc.Info.Trapped;

                // -----------------------------------------------------------------
                // 6. Copy any custom metadata entries (keys that are not predefined).
                // -----------------------------------------------------------------
                foreach (var kvp in sourceDoc.Info)
                {
                    if (DocumentInfo.IsPredefinedKey(kvp.Key))
                        continue; // already copied above

                    diffDoc.Info.Add(kvp.Key, kvp.Value);
                }

                // -----------------------------------------------------------------
                // 7. Save the diff PDF with the updated metadata.
                // -----------------------------------------------------------------
                diffDoc.Save(diffPath);
            }
        }

        Console.WriteLine($"Comparison completed. Metadata copied to '{diffPath}'.");
    }
}
