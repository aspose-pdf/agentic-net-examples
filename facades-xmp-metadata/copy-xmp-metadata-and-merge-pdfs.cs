using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string metadataSourcePath = "metadata_source.pdf"; // PDF containing the desired XMP metadata
        const string targetPdfPath      = "target.pdf";          // PDF that will receive the metadata
        const string pagesSourcePath    = "pages_source.pdf";    // PDF whose pages will be merged into the target
        const string tempTargetPath     = "temp_target.pdf";     // Intermediate file after metadata copy
        const string outputPath         = "merged_output.pdf";   // Final merged PDF

        // Ensure all input files exist
        if (!File.Exists(metadataSourcePath) ||
            !File.Exists(targetPdfPath) ||
            !File.Exists(pagesSourcePath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load source PDF (metadata) and target PDF, copy XMP metadata
        // ------------------------------------------------------------
        using (Document sourceMeta = new Document(metadataSourcePath))
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Aspose.Pdf does not expose an XmpMetadata property.
            // The XMP packet is stored in the Document.Metadata dictionary.
            // Clear any existing metadata on the target and copy all entries from the source.
            targetDoc.Metadata.Clear();
            foreach (var kvp in sourceMeta.Metadata)
            {
                targetDoc.Metadata.Add(kvp.Key, kvp.Value);
            }

            // Save the target with the new metadata to a temporary file.
            targetDoc.Save(tempTargetPath);
        }

        // ------------------------------------------------------------
        // 2. Merge pages from the pages source PDF into the target (now with metadata)
        //    using the PdfFileEditor facade.
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the temporary target (with metadata) and the pages source PDF.
        // The order determines that pages from pagesSourcePath follow the target's pages.
        editor.Concatenate(new[] { tempTargetPath, pagesSourcePath }, outputPath);

        // Clean up the intermediate file.
        try { File.Delete(tempTargetPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Merged PDF created at '{outputPath}'.");
    }
}
