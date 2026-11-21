using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "source.pdf";   // PDF whose pages and metadata will be used
        const string targetPdfPath = "target.pdf";   // PDF into which pages will be inserted
        const string outputPdfPath = "output.pdf";   // Final result

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Copy XMP metadata from source to target.
            //    Document.Metadata is a dictionary of string keys/values.
            // ------------------------------------------------------------
            foreach (var kvp in sourceDoc.Metadata)
            {
                // Overwrite or add each metadata entry in the target document
                targetDoc.Metadata[kvp.Key] = kvp.Value;
            }

            // ------------------------------------------------------------
            // 2. Insert pages from source into target.
            //    Aspose.Pdf.Facades.PdfFileEditor works with file paths,
            //    so we first save the modified target to a temporary file.
            // ------------------------------------------------------------
            string tempTargetPath = Path.Combine(Path.GetTempPath(),
                                                 Guid.NewGuid().ToString() + ".pdf");
            targetDoc.Save(tempTargetPath);

            // Create the editor (no using – PdfFileEditor does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Insert all pages of the source PDF after the last page of the target.
            // PdfFileEditor does not have a bulk‑insert method, so we concatenate
            // the two PDFs in the desired order (target first, then source).
            editor.Concatenate(tempTargetPath, sourcePdfPath, outputPdfPath);

            // ------------------------------------------------------------
            // 3. Ensure the metadata copied earlier is present in the final file.
            //    Concatenation may lose the metadata, so reload the output and
            //    re‑apply the metadata dictionary.
            // ------------------------------------------------------------
            using (Document finalDoc = new Document(outputPdfPath))
            {
                foreach (var kvp in targetDoc.Metadata)
                {
                    finalDoc.Metadata[kvp.Key] = kvp.Value;
                }
                finalDoc.Save(outputPdfPath);
            }

            // Clean up the temporary file
            try { File.Delete(tempTargetPath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"PDF created: {outputPdfPath}");
    }
}